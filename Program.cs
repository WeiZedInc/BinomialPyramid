namespace BinomialPyramid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                BinomialTree<float> tree = new BinomialTree<float>();

                // Вставка елементів
                tree.Insert(5.3f);
                tree.Insert(-3);
                tree.Insert(9.31f);
                tree.Insert(-1.241f);
                tree.Insert(12);
                tree.Insert(0);

                Console.WriteLine("Додано елементи: 5.3, -3, 9.31, -1.24, 12, 0");

                // Пошук будь-якого елементу
                float elementToSearch = 12;
                if (tree.Search(elementToSearch).Item1)
                {
                    Console.WriteLine("Елемент " + elementToSearch + " знайдено.");
                }
                else
                {
                    Console.WriteLine("Елемент " + elementToSearch + " не знайдено!");
                }

                // Видалення будь-якого елементу
                int elementToDelete = 12;
                tree.Remove(elementToDelete);
                Console.WriteLine("Елемент " + elementToDelete + " видалено.");
            }
        }
    }
}