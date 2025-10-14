# WordFinder

A C# class to find words in a character matrix.  
Supports horizontal (left-to-right) and vertical (top-to-bottom) searches, optimized for matrices up to 64x64.

## Usage Example

```csharp
var matrix = new[]
{
    "abcdc",
    "fgwio",
    "chill",
    "pqnsd",
    "uvdxy",
};

var words = new[] { "cold", "wind", "chill", "snow" };

var finder = new Finder();
finder.WordFinder(matrix);

var topWords = finder.Find(words);

Console.WriteLine("Top words found:");
foreach (var word in topWords)
    Console.WriteLine(word);
```

## Implementation Details

The Finder class provides two search implementations:

### 1. Implementation v1 (Simple)
- Concatenates all horizontal and vertical lines.  
- Iterates through each word and checks if it exists in each line using `Contains()`.  
- Easy to understand but less efficient for large word streams.

### 2. Implementation v2 (Optimized)
- Indexes the position of each letter in the matrix.  
- Groups words by their first letter.  
- Checks only the rows and columns that contain the first letter of a word.  
- Uses HashSets to avoid checking the same row or column multiple times.  
- Significantly reduces unnecessary searches and improves performance.

### Notes
- Both implementation runs under 500 microseconds for a 64x64 matrix

- Unit tests created in a separate project, test all the cases for x10 and x64 matrix