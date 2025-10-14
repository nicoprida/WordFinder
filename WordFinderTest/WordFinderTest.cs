using WordFinder;

namespace WordFinderTest
{
    public class Tests
    {
        IEnumerable<string> _matrix_x10;
        IEnumerable<string> _matrix_x64;
        Finder _finder;

        [SetUp]
        public void Setup()
        {
            _matrix_x10 = CreateMatrix_x10(); // Wind x2, Snow x1, Chill x1, House x1, Cold x1
            _matrix_x64 = CreateMatrix_x64(); // Wind x3, Snow x1, Chill x1, House x1, Cold x2,

            _finder = new Finder();
        }

        private static IEnumerable<TestCaseData> Data()
        {
            yield return new TestCaseData("x10");
            yield return new TestCaseData("x64");
        }

        private IEnumerable<string> GetMatrix(string matrixType)
        {
            return matrixType switch
            {
                "x10" => _matrix_x10,
                "x64" => _matrix_x64,
                _ => throw new ArgumentException("Tipo de matriz inválido")
            };
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void Find_2Words(string matrixType)
        {
            var wordsToFind = new List<string>() { "snow", "COLD" };

            _finder.WordFinder(GetMatrix(matrixType));
            var result = _finder.Find(wordsToFind);

            Assert.IsNotEmpty(result);
            Assert.True(result.Count() == 2);
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void Find_5Words(string matrixType)
        {
            var wordsToFind = new List<string>() { "snow", "chill", "wind", "HouSE", "COLD" };

            _finder.WordFinder(GetMatrix(matrixType));
            var result = _finder.Find(wordsToFind);

            Assert.IsNotEmpty(result);
            Assert.True(result.Count() == 5);
            Assert.IsTrue(result.ElementAt(0) == "WIND");
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void Find_5WordsWithExtra(string matrixType)
        {
            var wordsToFind = new List<string>() { "snow", "chill", "wind", "HouSE", "COLD", "cat", "dog", "tiger", "house" };

            _finder.WordFinder(GetMatrix(matrixType));
            var result = _finder.Find(wordsToFind);

            Assert.IsNotEmpty(result);
            Assert.True(result.Count() == 5);
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void Find_DuplicateWords(string matrixType)
        {
            var wordsToFind = new List<string>() { "snow", "snow", "snow", "snow" };

            _finder.WordFinder(GetMatrix(matrixType));
            var result = _finder.Find(wordsToFind);

            Assert.IsNotEmpty(result);
            Assert.True(result.Count() == 1);
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void Find_Top10Max(string matrixType)
        {
            var wordsToFind = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };

            _finder.WordFinder(GetMatrix(matrixType));
            var result = _finder.Find(wordsToFind);

            Assert.True(wordsToFind.Count() > 10);
            Assert.IsNotEmpty(result);
            Assert.True(result.Count() == 10);
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void Find_FindTop(string matrixType)
        {
            var wordsToFind = new List<string>() { "cold", "cat", "wind", "snow" };

            _finder.WordFinder(GetMatrix(matrixType));
            var result = _finder.Find(wordsToFind);

            Assert.IsNotEmpty(result);
            Assert.True(result.Count() == 3);
            Assert.IsTrue(result.ElementAt(0) == "WIND"); // Top 1
            Assert.IsTrue(result.ElementAt(1) == "COLD"); // Top 2
            Assert.IsTrue(result.ElementAt(2) == "SNOW"); // Top 3
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void Find_NoWords(string matrixType)
        {
            var wordsToFind = new List<string>() { "Dog", "Cat" };

            _finder.WordFinder(GetMatrix(matrixType));
            var result = _finder.Find(wordsToFind);

            Assert.IsEmpty(result);
        }

        private List<string> CreateMatrix_x10()
        {
            return new List<string>
            {
                "AXsnowRTGH",   // ← SNOW
                "BCQWEIOPAH",
                "chillQWERT",   // ← CHILL
                "DDASDFGcJK",   // ← COLD (Vertical)
                "EwindTYoIO",   // ← WIND
                "FOPASDFlHJ",
                "GLKJHOYdES",
                "HZXCVBNMAS",
                "IOhouseGFD",   // ← HOUSE
                "windXQWEDD"    // ← WIND 
            };
        }

        private List<string> CreateMatrix_x64()
        {
            return new List<string>
            {
                "QWERTYUIOPASDFGHJKLZXCVBNMASDFGHJKLCWERTYUIOPZXCVBNAAAAAAAAAAAAA", // ← "COLD" (Vertical)
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLOWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "ZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNMASLFGHJKLQWERTYUIOAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIODASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "LKJHGFDSAMNBVCXZQWERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMCOLDYUIOPQWERTYUIOPZXCVBNAAAAAAAAAAAAA", // ← "COLD"
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "ZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "LKJHGFDSAMNBVCXZQWERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMQWERTYUIOPZXCVBNMASDFGHJAAAAAAAAAAAAAA",
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "ZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTWINDAAAAAAAAAAAAA", // ← "WIND"
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "LKJHGFDSAMNBVCXZQWERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMQWERTYUIOPZXCVBNMASDFGHJAAAAAAAAAAAAAA",
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "ZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "LKJHGFDSAMNBVCXZQWERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMQWERTYUIOPZXCVBNMASDFGHJAAAAAAAAAAAAAA",
                "ASDFGHJKLQWERTYUIWPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA", // ← "WIND" (VERTICAL)
                "ZXCVBNMASDFGHJKLQIERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDNAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "LKJHGFDSAMNBVCXZQDERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMQWERTYUIOPZXCVBNMASDFGHJAAAAAAAAAAAAAA",
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "ZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "windGFDSAMNBVCXZQWERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA", // ← "WIND"
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMQWERTYUIOPZXCVBNMASDFGHJAAAAAAAAAAAAAA",
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "ZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "LKJHGFDSAMNBVCXZQWERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMQWERTYUIOPZXCVBNMASDFGHJAAAAAAAAAAAAAA",
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "ZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTCHILLAAAAAAAAAAAA", // ← "CHILL"
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "LKJHGFDSAMNBVCXZQWERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMQWERTYUIOPZXCVBNMASDFGHJAAAAAAAAAAAAAA",
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "ZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "LKJHGFDSAMNBVCXZQWERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMQWERTYUIOPZXCVBNMASDFGHJAAAAAAAAAAAAAA",
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "ZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNSNOWFGHJKLQWERTYUIOAAAAAAAAAAAAA", // ← "SNOW"
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "LKJHGFDSAMNBVCXZQWERTYUIOPQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "POIUYTREWQLKJHGFDSAMNBVCXZQWERTYUIOPASDFGHJKLZXCVBNAAAAAAAAAAAAA",
                "QWERTYUIOPASDFGHJKLZXCVBNMQWERTYUIOPZXCVBNMASDFGHJAAAAAAAAAAAAAA",
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
                "HOUSEBNMASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIAAAAAAAAAAAAA",  // ← "HOUSE"
                "ASDFGHJKLQWERTYUIOPZXCVBNMASDFGHJKLQWERTYUIOPZXCVBNAAAAAAAAAAAAA",
            };
        }
    }
}