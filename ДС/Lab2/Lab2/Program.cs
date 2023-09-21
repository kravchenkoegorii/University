namespace Lab2
{
    class Program
    {
        static void GenerateMenu()
        {
            Console.WriteLine("1 - Show");
            Console.WriteLine("2 - Add");
            Console.WriteLine("3 - Remove");
            Console.WriteLine("4 - Find");
            Console.WriteLine("5 - Clear");
            Console.WriteLine("0 - EXIT");
        }
        static void Main(string[] args)
        {
            IntSkipList userSkipList = new IntSkipList();
            int i = 1;
            int n;
            while (i != 0)
            {
                GenerateMenu();
                i = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (i)
                {
                    case 1:
                        userSkipList.Show();
                        break;
                    case 2:
                        Console.WriteLine("Input: ");
                        n = Convert.ToInt32(Console.ReadLine());
                        userSkipList.Insert(n);
                        break;
                    case 3:
                        Console.WriteLine("Input: ");
                        n = Convert.ToInt32(Console.ReadLine());
                        if (userSkipList.Remove(n) == true)
                            Console.WriteLine("Deleted");
                        else
                            Console.WriteLine("Not finded");
                        break;
                    case 4:
                        Console.WriteLine("Input: ");
                        n = Convert.ToInt32(Console.ReadLine());
                        if (userSkipList.Contains(n) == true)
                            Console.WriteLine("Finded");
                        else
                            Console.WriteLine("Not finded");
                        break;
                    case 5:
                        userSkipList.Clear();
                        break;
                }
            }
        }
    }
}