﻿using Xunit;

namespace BitStreams.Test
{
    public class ReadBitBasicTests
    {
        private const int FirstByte = 0b00101101;
        private const int SecondByte = 0b11011101;
        private const int ThirdByte = 0b10110101;

        private BitStream _testObj;

        public ReadBitBasicTests()
        {
            _testObj = BitStreamUtils.FromBytes(FirstByte, SecondByte, ThirdByte);
        }

        [Fact]
        public void ReadByteBitByBit()
        {
            byte result = 0;
            for (int i = 7; i >= 0; i--)
            {
                result |= (byte)(_testObj.ReadBit() << i);
            }

            Assert.Equal(FirstByte, result);
        }

        [Fact]
        public void Read7BitsThenByte_ReturnsCorrectByte()
        {
            for (int i = 6; i >= 0; i--)
            {
                _testObj.ReadBit();
            }

            int result = _testObj.ReadByte();

            Assert.Equal(0b11101110, result);
        }

        [Fact]
        public void ReadBitThenByte_ReturnsCorrectByte()
        {
            for (int i = 0; i >= 0; i--)
            {
                _testObj.ReadBit();
            }

            int result = _testObj.ReadByte();

            Assert.Equal(0b01011011, result);
        }

        [Fact]
        public void ReadBitThenBytes_ReturnsCorrectBytes()
        {
            for (int i = 0; i >= 0; i--)
            {
                _testObj.ReadBit();
            }

            int result = _testObj.ReadByte();
            int result2 = _testObj.ReadByte();

            Assert.Equal(0b01011011, result);
            Assert.Equal(0b10111011, result2);
        }
        [Fact]
        public void Read3BitsThenByte_ReturnsCorrectByte()
        {
            for (int i = 3; i >= 0; i--)
            {
                _testObj.ReadBit();
            }

            int result = _testObj.ReadByte();

            Assert.Equal(0b11011101, result);
        }

        [Fact]
        public void ReadBitOnEmptyBuffer_ResturnsEOS()
        {
            _testObj.ReadByte();
            _testObj.ReadByte();
            _testObj.ReadByte();

            int result = _testObj.ReadBit();

            Assert.Equal(-1, result);
        }

    }
}