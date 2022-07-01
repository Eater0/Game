[System.Serializable]
public class DataSettings
{
    public int difficultyLevel;
    public float volume;

    public DataSettings(int difficultyLevel, float volume)
    {
        this.difficultyLevel = difficultyLevel;
        this.volume = volume;
    }
}
