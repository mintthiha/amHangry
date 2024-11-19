namespace Test;
using UserNameSpace;
[TestClass]
public class UserTests
{

    [TestMethod]
    public void TestGetUsername()
    {
        //Arrange
        Member Mark = new("Mark", "test12");

        //Act
        string username = Mark.Username!;

        //Assert
        Assert.AreEqual("Mark", username);
    }

    [TestMethod]
    public void TestSetUsername()
    {
        //Arrange
        Member Mark = new("Mark", "test12");

        //Act
        Mark.Username = "Daniel";

        //Assert
        Assert.AreEqual("Daniel", Mark.Username);
    }

    [TestMethod]
    public void TestGetUsernameDescription()
    {
        //Arrange
        Member Mark = new("Mark", "test12")
        {
            UserDescription = "Mark is a user that Joined in 2020, he likes mostly caribbean cuisine"
        };

        //Act
        string userDescription = Mark.UserDescription;

        //Assert
        Assert.AreEqual("Mark is a user that Joined in 2020, he likes mostly caribbean cuisine", userDescription);
    }

    [TestMethod]
    public void TestSetUsernameDescription()
    {
        //Arrange
        Member Mark = new("Mark", "test12")
        {
            UserDescription = "Mark is a user that Joined in 2020, he likes mostly caribbean cuisine"
        };

        //Act
        Mark.UserDescription = "New Description Of Mark";

        //Assert
        Assert.AreEqual("New Description Of Mark", Mark.UserDescription);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
    "Validation for username min 3ch    //acters failed")]
    public void TestUserConstructor_Username_ThrowExecptionWhenLessThan3()
    {
        //Test the user constructor
        Member Mark = new("Do", "test12");
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
    "Validation for username max 20 ch    //acters failed")]
    public void TestUserConstructor_Username_ThrowExecptionWhenMoreThan20()
    {
        //Test the user constructor
        Member Mark = new("Noah Morgan-Paul Sebastian The Third", "test12");
    }

    [TestMethod]
    public void TestUserConstructor_Password_HashingAlgorithmWorksOnInitialiazation_PasswordNotSameAsValueGivenInConstructor()
    {
        //Test the user constructor
        Member Mark = new("Mark", "test12");

        Assert.AreNotEqual("test12", Mark.Password);
    }
    [TestMethod]
    public void TestMatchPassword_ReturnsTrue_SamePassword()
    {
        User Mark = new Member("Mark", "test12");
        Assert.IsTrue(Mark.MatchPasswords("test12"));
    }

    [TestMethod]
    public void TestMatchListOwnedRecipes(){

    }
  
}