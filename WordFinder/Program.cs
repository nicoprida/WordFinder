using WordFinder;

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

Console.ReadLine();