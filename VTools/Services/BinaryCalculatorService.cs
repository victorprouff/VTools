namespace VTools.Services;

public static class BinaryCalculatorService
{
    public static int BinaryToDecimal(long value)
    {
        var decimalNumber = Convert.ToString(value, 10);
        return Convert.ToInt32(decimalNumber, 2);
    }

    public static long DecimalToBinary(int value)
    {
        var binaryNumber = Convert.ToString(value, 2);
        return Convert.ToInt64(binaryNumber);
    }

    public static bool[] BinaryToBool(long value, int length)
    {
        var binary = value.ToString();
        binary = binary.PadLeft(length, '0');
        return binary.ToCharArray().Select(b => b == '1').ToArray();
    }
}