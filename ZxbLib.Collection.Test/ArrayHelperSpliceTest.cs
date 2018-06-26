using System;
using Xunit;
using ZxbLib.Collection.Helpers;

namespace ZxbLib.Collection.Test
{
    public class ArrayHelperSpliceTest
    {
        [Fact]
        public void ValidateInputArguments()
        {
            int[] anull = null;
            Assert.Throws<ArgumentNullException>(() => ArrayHelper.Splice(ref anull, 0, 0));

            int[] a = { 1, 2, 3 };
            // 0 <= sourceIndex < sourceArray.Length
            Assert.Throws<ArgumentOutOfRangeException>(() => ArrayHelper.Splice(ref a, -1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => ArrayHelper.Splice(ref a, a.Length, 0));
            // length >= 0
            Assert.Throws<ArgumentOutOfRangeException>(() => ArrayHelper.Splice(ref a, 0, -1));  
        }

        [Fact]
        public void TrancateOneElementButInputLengthIs2()
        {
            int[] a = { 1, 2, 3, 4, 5, 6 };

            int[] b = ArrayHelper.Splice(ref a, 5, 2);

            Assert.Equal(new int[] { 1, 2, 3, 4, 5 }, a);
            Assert.Equal(new int[] { 6 }, b);
        }

        [Fact]
        public void TrancateTwoElements()
        {
            int[] a = { 1, 2, 3, 4, 5, 6 };

            int[] b = ArrayHelper.Splice(ref a, 2, 2);

            Assert.Equal(new int[] { 1, 2, 5, 6 }, a);
            Assert.Equal(new int[] { 3, 4 }, b);
        }

        [Fact]
        public void TrancateAllElements()
        {
            int[] a = { 1, 2, 3, 4, 5, 6 };

            int[] b = ArrayHelper.Splice(ref a, 0, a.Length);

            Assert.Equal(new int[] { }, a);
            Assert.Equal(new int[] { 1, 2, 3, 4, 5, 6 }, b);
        }

        [Fact]
        public void TrancateArrayAndInsertNewElements()
        {
            int[] a = { 1, 2, 3, 4, 5, 6 };

            int[] b = ArrayHelper.Splice(ref a, 2, 2, 7, 8, 9);

            Assert.Equal(new int[] { 1, 2, 7, 8, 9, 5, 6 }, a);
            Assert.Equal(new int[] { 3, 4 }, b);
        }
    }
}
