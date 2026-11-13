using System;
using System.Collections.Generic;
using System.Text;

namespace OOPProj1Backend;

// Reusable Validation
public static class WordCountValidator
{
    // Validate the input text
    public static void ValidateText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Text cannot be empty.", nameof(text));
    }

    // Validate - Different responsibilities
    public static void ValidateKeyword(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            throw new ArgumentException("Keyword cannot be empty.", nameof(keyword));
    }
}
