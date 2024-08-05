using FluentAssertions;

namespace DevTdd.Course.UnitTests
{
    public class PasswordValidationShould
    {
        private readonly PasswordValidator _passwordValidator;

        public PasswordValidationShould()
        {
            _passwordValidator = new PasswordValidator();
        }

        [Theory]
        [InlineData("A1b_c")]
        [InlineData("A1_defg")]
        public void ThrowArgumentExceptionForShortPasswords(string password)
        {
            Assert.Throws<ArgumentException>(() => _passwordValidator.ValidatePassword(password));
        }

        [Theory]
        [InlineData("ABCDEfg1_")]
        [InlineData("aBCDEfg1_")]
        [InlineData("_BC1Efg_")]
        public void BeValid(string password)
        {
            var result = _passwordValidator.ValidatePassword(password);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("abcdefg1_")]
        [InlineData("abcdefg2_")]
        [InlineData("abcdefg3_")]
        public void BeInvalidNoUppercase(string password)
        {
            var result = _passwordValidator.ValidatePassword(password);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("abcDEFGA_")]
        [InlineData("_bcdEFGAa")]
        [InlineData("abcdeFGA_")]
        public void BeInvalidNoLowercase(string password)
        {
            var result = _passwordValidator.ValidatePassword(password);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("aBCDEFG3a")]
        [InlineData("abCDEFG3a")]
        [InlineData("abcDEFG3a")]
        public void BeInvalidNoUnderscore(string password)
        {
            var result = _passwordValidator.ValidatePassword(password);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("abcDEFGA_")]
        [InlineData("_bcdEFGAa")]
        [InlineData("abcdeFGA_")]
        public void BeInvalidNoNumbers(string password)
        {
            var result = _passwordValidator.ValidatePassword(password);

            result.Should().BeFalse();
        }

    }

    public class PasswordValidator
    {
        public bool ValidatePassword(string password)
        {
            if (password.Length < 8)
                throw new ArgumentException();

            var hasLowerCase = false;
            var hasUnderscore = false;
            var hasNumber = false;
            var hasUppercase = false;
            foreach (var character in password)
            {
                if (char.IsLower(character))
                    hasLowerCase = true;

                if (char.IsUpper(character))
                    hasUppercase = true;

                if (character == '_')
                    hasUnderscore = true;

                if (char.IsDigit(character))
                    hasNumber = true;
            }

            return hasLowerCase && hasUnderscore && hasNumber && hasUppercase;
        }
    }
}
