using Xunit;

namespace TestHelpers
{
    public static class TestHelper
    {
        public static void AssertThrowsException(Action action, string input, bool isNull)
        {
            if (isNull)
            {
                Assert.Throws<ArgumentNullException>(action);
            }
            else
            {
                Assert.Throws<ArgumentException>(action);
            }
        }
    }
}
