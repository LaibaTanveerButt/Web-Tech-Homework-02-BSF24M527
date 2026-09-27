using LibraryData;
using System.Net;
namespace LibraryApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n-------Menu Driven-------");
            while (true)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("1. Add Book  \n2. View All Books  \n3. Add Member  \n4. View All Members  \n5. Issue Book to Member (Transaction)  \n6. Return Book (Transaction)\r\n7. Export Books to JSON  \n8. Import Books from JSON  \n9. Exit\n\n");
                Console.Write("Enter option: ");
                int option = int.Parse(Console.ReadLine());

                Console.WriteLine("\n");

                if (option == 1)
                {
                    Console.Write("Enter book id: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();

                    Console.Write("Enter book author: ");
                    string author = Console.ReadLine();

                    Console.Write("Enter book stock: ");
                    int stock = int.Parse(Console.ReadLine());


                    Book book = new Book(id, title, author, stock);

                    BookRepository repo = new BookRepository();
                    repo.AddBook(book);
                }

                else if (option == 2)
                {
                    BookRepository repo = new BookRepository();
                    List<Book> list = repo.GetAllBooks();

                    foreach (Book book in list)
                    {
                        Console.WriteLine($"Book id : {book.BookId}");
                        Console.WriteLine($"Book title is: {book.Title}");
                        Console.WriteLine($"Book Author is: {book.Author}");
                        Console.WriteLine($"Book stock is: {book.Stock}");
                        Console.WriteLine("------------------------\n");
                    }

                }
                else if (option == 3)
                {
                    Console.Write("Enter member id: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("Enter member name: ");
                    string name = Console.ReadLine();

                    Member member = new Member(id, name);

                    MemberRepository repo = new MemberRepository();
                    repo.AddMember(member);

                }

                else if (option == 4)
                {
                    MemberRepository repo = new MemberRepository();
                    List<Member> list = repo.GetAllMembers();

                    foreach (Member member in list)
                    {
                        Console.WriteLine($"Member id is: {member.MemberId}");
                        Console.WriteLine($"Member name is: {member.Name}");
                        Console.WriteLine("------------------------");
                    }

                }

                else if (option == 5)
                {
                    Console.Write("Enter book id you want to issue: ");
                    int bookid = int.Parse(Console.ReadLine());

                    Console.Write("Enter member id: ");
                    int memberid = int.Parse(Console.ReadLine());

                    LibData libdata = new LibData();
                    string result = libdata.IssueBook(bookid, memberid);

                    Console.WriteLine(result);

                }

                else if (option == 6)
                {
                    Console.Write("Enter issue id you want to return: ");
                    int issueid = int.Parse(Console.ReadLine());

                    LibData libdata = new LibData();
                    string result = libdata.ReturnBook(issueid);

                    Console.WriteLine(result);

                }

                else if (option == 7)
                {
                    Console.WriteLine("-------------Continuously add books to list for creating backup---------");
                    List<Book> list = new List<Book>();

                    while (true)
                    {
                        Console.Write("Enter book id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter book author: ");
                        string author = Console.ReadLine();

                        Console.Write("Enter book stock: ");
                        int stock = int.Parse(Console.ReadLine());

                        Book book = new Book(id, title, author, stock);
                        list.Add(book);

                        Console.Write("Press 'exit' to stop adding books, or press Enter to add another: ");
                        string exit = Console.ReadLine();

                        if (exit.Trim().ToLower() == "exit")
                        {
                            break;
                        }
                    }

                    BackupHelper helper = new BackupHelper();
                    Console.Write("Enter full file path where you want to create backup (e.g. backup.json): ");
                    string path = Console.ReadLine();
                    helper.ExportBooks(list, path);
                }

                else if (option == 8)
                {
                    Console.Write("Enter file path from where you want to view backup: ");
                    string path = Console.ReadLine();

                    BackupHelper helper = new BackupHelper();

                    
                    List<Book> restoredBooks = helper.ImportBooks(path);

                   
                    Console.WriteLine("\n--- Restored JSON Inventory Data ---");
                    Console.WriteLine("-------------------------------------------------");
                    foreach (Book book in restoredBooks)
                    {
                        Console.WriteLine($"Title: {book.Title,-30} | Stock: {book.Stock}");
                    }
                    Console.WriteLine("-------------------------------------------------\n");
                }


                else if (option == 8)
                {
                    Console.WriteLine("Enter file path from where you want to view backup:");
                    string path = Console.ReadLine();

                    BackupHelper helper = new BackupHelper();
                    helper.ImportBooks(path);
                }

                else if (option == 9)
                {
                    Console.WriteLine("Exiting Program .....!!!!!!");
                    break;
                }

                else
                {
                    Console.WriteLine("Please enter a correct option.......from (1-9)");
                }

            }
                
        }
        
    }
}
