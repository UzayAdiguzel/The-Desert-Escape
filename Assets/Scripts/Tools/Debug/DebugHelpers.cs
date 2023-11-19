﻿using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class DebugHelpers
{
    private static string GetColor(Color color) => ColorUtility.ToHtmlStringRGBA(color);

    #region Object

    public static string GetMessage(object str)
    {
        var htmlColor = GetColor(DebugKeys.TitleColor);
        var msg = $"<color=#{htmlColor}> <<{DebugKeys.Title}>> </color> {GetColoredMessage(str)}";
        return msg;
    }

    public static string GetMessage(object str, Color color)
    {
        var htmlColor = ColorUtility.ToHtmlStringRGBA(DebugKeys.TitleColor);
        var msg = $"<color=#{htmlColor}> <<{DebugKeys.Title}>> </color>" +
                  GetPureMessage(str, color);
        return msg;
    }

    private static string GetPureMessage(object str, Color color)
    {
        var htmlColor = GetColor(color);
        return $"<color=#{htmlColor}>{str}</color>";
    }

    private static string GetColoredMessage(object str)
    {
        var message = str.ToString();

        var regexStr =
            @"RGBA?\((?<R>\s*)([01]\.?\d*?),(?<G>\s*)([01]\.?\d*?),(?<B>\s*)([01]\.?\d*?),(?<A>\s*)([01]\.?\d*?)\)"; //@"RGBA(([^)]+))";
        var matches = Regex.Matches(message, regexStr, RegexOptions.IgnoreCase);
        if (matches.Count <= 0)
            return message;

        List<Color> colorList = new List<Color>();

        if (matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    float r = float.Parse(match.Groups[1].Value);
                    float g = float.Parse(match.Groups[2].Value);
                    float b = float.Parse(match.Groups[3].Value);
                    float a = float.Parse(match.Groups[4].Value);

                    Color color = new Color(r, g, b, a);
                    colorList.Add(color);
                }
            }
        }

        var newMessage = Regex.Split(message, @"RGBA(([^)]+)\))");
        var regexNewStr =
            @"\((?<R>\s*)([01]\.?\d*?),(?<G>\s*)([01]\.?\d*?),(?<B>\s*)([01]\.?\d*?),(?<A>\s*)([01]\.?\d*?)";
        List<string> messageList = new List<string>();
        int colorCounter = 0;
        foreach (var s in newMessage)
        {
            if (string.IsNullOrEmpty(s))
                continue;

            var isMatch = Regex.IsMatch(s, regexNewStr);
            if (isMatch)
                continue;

            var color = colorList[colorCounter];
            messageList.Add(GetPureMessage(s, color));

            colorCounter++;
            if (colorCounter >= colorList.Count)
                colorCounter--;
        }

        StringBuilder sb = new StringBuilder();
        foreach (var msg in messageList)
            sb.Append(msg);

        return sb.ToString();
    }

    #endregion

    #region String

    public static string GetMessage(string str)
    {
        var htmlColor = GetColor(DebugKeys.TitleColor);
        var msg = $"<color=#{htmlColor}> <<{DebugKeys.Title}>> </color> {GetColoredMessage(str)}";
        return msg;
    }

    public static string GetMessage(string str, Color color)
    {
        var htmlColor = ColorUtility.ToHtmlStringRGBA(DebugKeys.TitleColor);
        var msg = $"<color=#{htmlColor}> <<{DebugKeys.Title}>> </color>" +
                  GetPureMessage(str, color);
        return msg;
    }

    private static string GetPureMessage(string str, Color color)
    {
        var htmlColor = GetColor(color);
        return $"<color=#{htmlColor}>{str}</color>";
    }

    private static string GetColoredMessage(string str)
    {
        var message = str;

        var regexStr =
            @"RGBA?\((?<R>\s*)([01]\.?\d*?),(?<G>\s*)([01]\.?\d*?),(?<B>\s*)([01]\.?\d*?),(?<A>\s*)([01]\.?\d*?)\)"; //@"RGBA(([^)]+))";
        var matches = Regex.Matches(message, regexStr, RegexOptions.IgnoreCase);
        if (matches.Count <= 0)
            return message;

        List<Color> colorList = new List<Color>();

        if (matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    float r = float.Parse(match.Groups[1].Value);
                    float g = float.Parse(match.Groups[2].Value);
                    float b = float.Parse(match.Groups[3].Value);
                    float a = float.Parse(match.Groups[4].Value);

                    Color color = new Color(r, g, b, a);
                    colorList.Add(color);
                }
            }
        }

        var newMessage = Regex.Split(message, @"RGBA(([^)]+)\))");
        var regexNewStr =
            @"\((?<R>\s*)([01]\.?\d*?),(?<G>\s*)([01]\.?\d*?),(?<B>\s*)([01]\.?\d*?),(?<A>\s*)([01]\.?\d*?)";
        List<string> messageList = new List<string>();
        int colorCounter = 0;
        foreach (var s in newMessage)
        {
            if (string.IsNullOrEmpty(s))
                continue;

            var isMatch = Regex.IsMatch(s, regexNewStr);
            if (isMatch)
                continue;

            var color = colorList[colorCounter];
            messageList.Add(GetPureMessage(s, color));

            colorCounter++;
            if (colorCounter >= colorList.Count)
                colorCounter--;
        }

        StringBuilder sb = new StringBuilder();
        foreach (var msg in messageList)
            sb.Append(msg);

        return sb.ToString();
    }

    #endregion
}