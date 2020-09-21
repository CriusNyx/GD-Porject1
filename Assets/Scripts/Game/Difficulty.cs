using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty
{
    public enum DifficultyLevel
    {
        VeryEasy,
        Easy,
        Normal,
        Hard,
        VeryHard
    }

    /// <summary>
    /// The difficulty level the game will enter after a reset
    /// </summary>
    public static DifficultyLevel baseDifficultyLevel { get; private set; } = DifficultyLevel.Normal;

    /// <summary>
    /// The current difficulty level of the game
    /// </summary>
    public static DifficultyLevel currentDifficultyLevel { get; private set; } = DifficultyLevel.Normal;

    /// <summary>
    /// Set the base difficulty level.
    /// The base difficulty level is the level selected by the player
    /// This will be the difficulty level after a reset
    /// </summary>
    /// <param name="difficultyLevel"></param>
    public static void SetBaseDifficultyLevel(DifficultyLevel difficultyLevel, bool reset = true)
    {
        baseDifficultyLevel = difficultyLevel;
        if (reset)
        {
            ResetDifficultyLevel();
        }
    }

    /// <summary>
    /// Sets the difficulty level back to the level selected by the player.
    /// </summary>
    /// <param name="difficultyLevel"></param>
    public static void ResetDifficultyLevel()
    {
        currentDifficultyLevel = baseDifficultyLevel;
    }

    /// <summary>
    /// Increases the difficulty level by one.
    /// Returns true if it is able to inrement the difficulty level
    /// </summary>
    public static bool IncrementDifficultyLevel()
    {
        var newValue = currentDifficultyLevel + 1;
        if (System.Enum.IsDefined(typeof(DifficultyLevel), newValue))
        {
            currentDifficultyLevel = newValue;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Returns the appropriate value for the given difficulty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="veryEasy"></param>
    /// <param name="easy"></param>
    /// <param name="normal"></param>
    /// <param name="hard"></param>
    /// <param name="veryHard"></param>
    /// <returns></returns>
    public static T GetValue<T>(T veryEasy, T easy, T normal, T hard, T veryHard)
    {
        switch (currentDifficultyLevel)
        {
            case DifficultyLevel.VeryEasy:
                return veryEasy;
            case DifficultyLevel.Easy:
                return easy;
            case DifficultyLevel.Normal:
                return normal;
            case DifficultyLevel.Hard:
                return hard;
            case DifficultyLevel.VeryHard:
                return veryHard;
            default:
                throw new System.InvalidOperationException($"The current difficulty level {currentDifficultyLevel} is not a known difficulty level.");
        }
    }
}