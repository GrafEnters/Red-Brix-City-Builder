public static class TimeStringConverter
{
    // ���������� ������ � ������� 00d 00h 00m 00s, �������� �� ������� ��������
    public static string GetTimeString(int seconds)
    {
        int days = seconds / (3600 * 24);
        int hours = (seconds % (3600 * 24)) / 3600;
        int mins = (seconds % 3600) / 60;
        int secs = seconds % 60;
        if (days > 0)
            return days + "d " + hours + "h";
        else if (hours > 0)
            return hours + "h " + mins + "m";
        else
            return mins + "m " + secs + "s";
    }
}
