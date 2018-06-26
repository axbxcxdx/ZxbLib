using System;

namespace ZxbLib.Collection.Helpers
{
    public static class ArrayHelper
    {
        // 实现Javascript Splice函数的功能
        public static T[] Splice<T>(ref T[] sourceArray, int sourceIndex, int length, params T[] insertedElements)
        {
            if (sourceArray == null)
            {
                throw new ArgumentNullException(nameof(sourceArray));
            }
            if (sourceIndex < 0 || sourceIndex >= sourceArray.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(sourceIndex));
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            // 如果要截取的元素个数大于从sourceIndex开始到数组结束的元素个数
            int acturalLength = (sourceIndex + length) > sourceArray.Length ? (sourceArray.Length - sourceIndex) : length;

            var deletedItems = new T[acturalLength];
            Array.ConstrainedCopy(sourceArray, sourceIndex, deletedItems, 0, acturalLength);

            int arrayLengthDifference = insertedElements.Length - acturalLength;
            int newArrayLength = sourceArray.Length + arrayLengthDifference;
            var newArray = new T[newArrayLength];

            Array.Copy(sourceArray, newArray, sourceIndex);
            int newArrayCopyedElementsIndex = sourceIndex;
            Array.ConstrainedCopy(insertedElements, 0, newArray, newArrayCopyedElementsIndex, insertedElements.Length);
            newArrayCopyedElementsIndex += insertedElements.Length;
            int remainedElementsIndex = sourceIndex + acturalLength;
            // 源数组末尾还有元素才进行拷贝
            if (remainedElementsIndex < sourceArray.Length)
            {
                Array.ConstrainedCopy(sourceArray, remainedElementsIndex, newArray, newArrayCopyedElementsIndex, sourceArray.Length - remainedElementsIndex);
            }

            sourceArray = newArray;

            return deletedItems;
        }
    }
}
