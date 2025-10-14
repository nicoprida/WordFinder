using System.Text;

namespace WordFinder
{
    public class Finder : IWordFinder
    {
        string[] _horizontalLines;
        string[] _verticalLines;
        Dictionary<string, int> _wordsCount;
        Dictionary<char, List<(int X, int Y)>> _letterPositions;

        public Finder()
        {
            _wordsCount = new Dictionary<string, int>();
            _letterPositions = new Dictionary<char, List<(int X, int Y)>>();
        }

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            // Initialize all the words to 0, remove the repeated
            foreach (var word in wordStream)
            {
                var wordUpper = word.ToUpper();
                if (!_wordsCount.ContainsKey(wordUpper))
                    _wordsCount.Add(wordUpper, 0);
            }

            Implementation_v1(); 
            //Implementation_v2();

            return _wordsCount.Where(x => x.Value > 0).OrderByDescending(x => x.Value).Select(x => x.Key).Take(10);
        }

        public void WordFinder(IEnumerable<string> matrix)
        {
            _wordsCount.Clear();
            _letterPositions.Clear();

            var lines = matrix.ToArray();
            var totalLines = lines.Length;
            _horizontalLines = new string[totalLines];
            _verticalLines = new string[totalLines];

            // Init string builder array
            var verticalLinesSB = new StringBuilder[totalLines];
            for (int i = 0; i < _verticalLines.Length; i++)
                verticalLinesSB[i] = new StringBuilder();

            for (int x = 0; x < totalLines; x++)
            {
                var line = lines[x].ToUpper();

                if (line.Length != totalLines)
                    throw new Exception("Matrix length is wrong");

                _horizontalLines[x] = line;

                for (int y = 0; y < line.Length; y++)
                {
                    var letter = line[y];
                    verticalLinesSB[y].Append(letter);
                    StoreLetterPosition(letter, x, y);
                }
            }

            _verticalLines = verticalLinesSB.Select(sb => sb.ToString()).ToArray();
        }

        private void StoreLetterPosition(char letter, int xPos, int yPos)
        {
            if (_letterPositions.TryGetValue(letter, out var position))
            {
                position.Add((xPos, yPos));
                return;
            }

            _letterPositions.Add(letter, new List<(int X, int Y)>() { (xPos, yPos) });
        }

        // v1: Simple solution
        private void Implementation_v1()
        {
            var allLines = _horizontalLines.Concat(_verticalLines);

            foreach (var word in _wordsCount.Keys)
            {
                foreach (var line in allLines)
                {
                    if (line.Contains(word))
                        _wordsCount[word]++;
                }
            }
        }

        // v2: Smart search: Look for letter position
        private void Implementation_v2()
        {
            // Collect the first letter of all words and group them
            var firstLetterWords = new Dictionary<char, List<string>>();
            foreach (var word in _wordsCount.Keys)
            {
                if (!firstLetterWords.TryGetValue(word[0], out var words))
                    firstLetterWords.Add(word[0], new List<string>() { word });
                else
                    words.Add(word);
            }

            foreach (var wordItem in firstLetterWords)
            {
                var letter = wordItem.Key;
                var words = wordItem.Value;

                if (!_letterPositions.TryGetValue(letter, out var positions))
                    continue;

                var alreadyCheckX = new HashSet<int>();
                var alreadyCheckY = new HashSet<int>();

                foreach (var position in positions)
                {
                    var xPos = position.X;
                    var yPos = position.Y;

                    if (!alreadyCheckX.Contains(xPos))
                    {
                        alreadyCheckX.Add(xPos);
                        CheckLines(words, _horizontalLines[xPos]);
                    }

                    if (!alreadyCheckY.Contains(yPos))
                    {
                        alreadyCheckY.Add(yPos);
                        CheckLines(words, _verticalLines[yPos]);
                    }
                }
            }
        }

        private void CheckLines(IEnumerable<string> words, string line)
        {
            foreach (var word in words)
                if (line.Contains(word))
                    _wordsCount[word]++;
        }
    }
}
