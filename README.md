# xUnit Test Documentation for Student Management API

This documentation outlines the xUnit tests implemented for the Student Management API, specifically focusing on the `StudentController` class and its `CreateStudent` method. The tests verify the functionality, error handling, and input validation of the API.

## Test Structure

- **Test Class**: A test class is created for the `StudentController`, focusing on a single API. An additional xUnit test project is created, with the test methods located in the `UnitTest1` class.
- **Test Methods**: The main focus is on three types of testing procedures:
  - **Manual Testing**: In this process, the developer provides hardcoded data and tests the entire method.
    - **Example**:
      ```csharp
      [Fact]
      public async Task CreateStudent_ValidStudent_ReturnsCreatedResult()
      {
          // Arrange: Create a valid student object
          var student = new Student
          {
              Id = 1,
              Name = "John",
              Email = "john.doe@example.com",
              Age = 20
          };

          // Act: Call the CreateStudent method
          var result = await _controller.CreateStudent(student);

          // Assert: Verify the expected result and behavior
          var actionResult = Assert.IsType<CreatedAtActionResult>(result);
          Assert.Equal("GetStudentList", actionResult.ActionName); // Checks the named action
          _mockRepo.Verify(repo => repo.CreateStudent(student), Times.Once); // Checks that the repository method is called once
      }
      ```

  - **InlineData Procedure**: This procedure uses hardcoded data as attributes and specifies the expected error messages.
    - **Example**:
      ```csharp
      [Theory]
      [InlineData(null, "Email", 20, "The Name field is required.")]
      [InlineData("John", null, 20, "The Email field is required.")]
      [InlineData("John", "john@example.com", 150, "The Age must be a positive number.")]
      public async Task CreateStudent_InvalidData_ReturnsBadRequest_By_HardData(string name, string email, int age, string expectedError)
      {
          // Arrange
          var student = new Student { Name = name, Email = email, Age = age };

          // Act
          var result = await _controller.CreateStudent(student);

          // Assert
          var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
          var errors = badRequestResult.Value as ModelStateDictionary;
          Assert.True(errors.Values.Any(v => v.Errors.Any(e => e.ErrorMessage == expectedError)), "Expected error message not found.");
      }
      ```

  - **Bogus**: This process does not require hardcoded values; it automatically generates data for testing.
    - **Example**:
      ```csharp
      [Theory]
      [MemberData(nameof(GenerateDynamicInvalidStudents))]
      public async Task CreateStudent_InvalidData_ReturnsBadRequest(Student student, string expectedError)
      {
          // Arrange: Force model validation failure based on properties
          if (string.IsNullOrEmpty(student.Name))
          {
              _controller.ModelState.AddModelError("Name", "The Name field is required.");
          }
          if (string.IsNullOrEmpty(student.Email))
          {
              _controller.ModelState.AddModelError("Email", "The Email field is required.");
          }
          if (student.Age < 0)
          {
              _controller.ModelState.AddModelError("Age", "The Age must be a positive number.");
          }

          // Act: Call the CreateStudent method
          var result = await _controller.CreateStudent(student);

          // Assert: Verify the result is a BadRequest
          var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
          Assert.Contains(expectedError, badRequestResult.Value.ToString());
      }

      public static IEnumerable<object[]> GenerateDynamicInvalidStudents()
      {
          var faker = new Faker<Student>()
              .RuleFor(s => s.Name, f => f.Name.FullName())
              .RuleFor(s => s.Email, f => f.Internet.Email())
              .RuleFor(s => s.Age, f => f.Random.Int(1, 100));

          // Generate invalid cases
          yield return new object[] { faker.Clone().RuleFor(s => s.Name, "").Generate(), "The Name field is required." };
          yield return new object[] { faker.Clone().RuleFor(s => s.Email, "").Generate(), "The Email field is required." };
          yield return new object[] { faker.Clone().RuleFor(s => s.Age, -1).Generate(), "The Age must be a positive number." };
      }
      ```

