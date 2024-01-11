public class FormulaCalculator
{
    public static float GetWPM(float _charLenght, float time)
    {
        return _charLenght / (time / 60);
    }

    public static float GetMeanWPM(float _currentWPM, float _lastWPM)
    {
        return (_currentWPM + _lastWPM) / 2;
    }

    public static float GetEPM(float _currentHit, float imte)
    {

        return _currentHit / (imte/60);
    }

    public static float GetAccuracy(float _correctHit, float _totalHit)
    {
        return (_correctHit / _totalHit)*100;
    }
}