using System;
namespace ClubMembershipApplication.Views;

class Program
{
    static void Main(string[] args) 
    {
        IView mainView = Factory.GetMainViewObject();
        mainView.RunView();

        Console.ReadKey();
    }
}
