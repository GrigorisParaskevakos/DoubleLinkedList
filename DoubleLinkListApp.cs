using System;
using System.IO;

// Το πρόγραμμα υλοποιεί μία δυναμική λίστα διπλής κατεύθυνσης 
//στην οποία εισάγεται ένα κείμενο σε μορφή αρχείου.
//Το πρόγραμμα  διαβάζει όλα τα σύμβολα και τους χαρακτήρες του κειμένου του αρχείου έναν έναν
//και εμφανίζει ταξινομημένα(αύξουσα) τη συχνότητα των χαρακτήρων  με βάση τον αριθμό ASCII κάθε χαρακτήρα καθώς και την συχνότητα εμφάνισης των χαρακτήρων με βάση το πλήθος τους.
//η ταξινόμηση των χαρακτήρων έγινε με την χρήση του αλγόριθμου Bubble Short.

namespace DoubleLinkedListAppGP
{

    class DListNode //Δημιουργούμε τον κόμβο της λίστας & δηλώνουμε τα χαρακτηριστικά του
    {
        public char data; // χαρακτήρας 
        public int n = 0; // πλήθος χαρακτήρων 
        public DListNode next; // δείκτης στην επόμενη θέση της λίστας
        public DListNode prev; // δείκτης στην προηγούμενη θέση της λίστας
    }

    class MyDoubleList //Δημιουργεί μια Διπλά συνδεδεμένη λίστα με τις πράξεις τις
    {
        private DListNode head; //ειδικός δείκτης πρώτου στοιχείου της λίστας

        public MyDoubleList() // O Δημιουργός αρχικοποιεί το  HEAD σε NULL
        {
            head = null;
        }

        public void insertFirst(char a) //εισάγει νέο κόμβο στην αρχή της λίστας 
        {
            // Δημιουργία νέου κόμβου
            DListNode tmp = new DListNode();

            // Τιμές των γνωρισμάτων του κόμβου
            tmp.data = a;    //εισάγει τα δεδομένα
            tmp.n++;         //αυξάνει το πλήθος των δεδομένων
            tmp.next = head; // αλλάζει το δείκτη του επόμενου να δείχνει στο head
            tmp.prev = null; // αλλάζει τον δείκτη του προηγούμενου να δείχνει σε null
            head = tmp;     // αλλάζει την τιμή του head της λίστας
        } //τέλος insertFirst

        public void removeFirst() //διαγράφει ένα κόμβο από την αρχή της λίστας 
        {
            // αν η λίστα δεν είναι κενή
            if (head != null) head = head.next;
            else Console.WriteLine("Η λίστα είναι κενή");
            // αν υπάρχει 2ος κόμβος, άλλαξε το prev σε null
            if (head != null) head.prev = null;
        } //τέλος removeFirst

        public void insertLast(char a) //εισάγει ένα νέο κόμβο στο τέλος της λίστας
        {
            // αν η λίστα είναι κενή
            if (head == null)
            {
                insertFirst(a);
            }
            else
            {
                DListNode tmp = head;
                //διασχίζει τη λίστα μέχρι το τέλος
                while (tmp.next != null)
                {
                    tmp = tmp.next;
                    DListNode p = new DListNode(); // δημιουργεί  νέο κόμβο
                    tmp.next = p;
                    p.data = a;
                    p.n++;
                    p.next = null;
                    p.prev = tmp;
                }
            }
        } //τέλος insertLast

        public void RemoveLast() // Αφαιρεί τον κόμβο από το τέλος της λίστας
        {
            if (head == null)
            {
                Console.WriteLine("NOTHING IN LIST");
            }
            else if (head.next == null)
            {
                removeFirst();
            }
            else
            {
                DListNode tmp = head;
                while (tmp.next != null)//Διασχίζει μέχρι το τέλος
                {
                    tmp = tmp.next;
                    tmp.prev.next = null;
                }
            }
        } //τέλος RemoveLast

        public bool charListExists(char a, out DListNode pointer) //επιστρέφει true αν υπάρχει ο χαρακτήρας στη λίστα καθώς και τη θέση του χαρακτήρα, αλλιώς false 
        {
            bool found = false;
            pointer = null;
            if (!(head == null))
            {
                DListNode tmp = head;
                while (!found && tmp != null) // Διασχίζει όλη τη λίστα μέχρι το τέλος
                    if (tmp.data == a)
                    {
                        found = true;
                        pointer = tmp;
                    }
                    else tmp = tmp.next;
            }
            return found;
        } //τέλος charListExists




        public void increaseCharcNumFound(ref DListNode c) //προσθέτει συν ένα στον κόμβο που υπάρχει ο χαρακτήρας 
        {
            c.n++;
        } //τέλος increaseCharcNumFound

        static void swapData(DListNode i, DListNode j) //αλλάζει τη θέση 2 χαρακτήρων της λίστας
        {
            char tmpChar = i.data;
            int tmpNum = i.n;

            i.data = j.data;
            i.n = j.n;
            j.data = tmpChar;
            j.n = tmpNum;
        } //τέλος swapData

        public void shortListCharsCount() //ταξινομεί τους χαρακτήρες της λίστας ως προς τον αριθμό ASCII που τους αντιστοιχεί με τον αλγόριθμο Bubble sort
        {
            DListNode j = null;
            DListNode piv = null;
            char min;

            for (DListNode i = head; i != null; i = i.next)
            {
                min = Convert.ToChar(127); // με βάση τον ΑSCII το μέγιστο πλήθος διαφορετικών χαρακτήρων-συμβόλων είναι 127
                for (j = i; j != null; j = j.next)
                {
                    if (j.data < min)
                    {
                        min = j.data;
                        piv = j;
                    }
                }
                swapData(i, piv);
            }
        } //τέλος shortListCharsCount

        public void shortListIntCount() // ταξινομεί τους χαρακτήρες της λίστας ως προς την συχνότητα εμφάνισής τους με τον αλγόριθμο bubble sort  
        {
            DListNode j = null;
            DListNode piv = null;

            for (DListNode i = head; i != null; i = i.next)
            {
                int max = Int32.MinValue;

                for (j = i; j != null; j = j.next)
                {
                    if (j.n > max)
                    {
                        max = j.n;
                        piv = j;
                    }
                }
                swapData(i, piv);
            }
        } //τέλος shortListIntCount



        public void traverseDlist(float allChars) // διατρέχει όλη λίστα και εμφανίζει τα δεδομένα
        {
            int counter = 0;
            for (DListNode i = head; i != null; i = i.next)
            {
                counter++;
                if (!(i.data == ' '))
                {
                    Console.Write("{0})    {1} ", counter, i.data);
                }
                else
                {
                    Console.Write("{0}) {1}", counter, "space");
                }
                float freq = i.n / allChars; //υπολογίζει τη συχνότητα εμφάνισης
                Console.WriteLine("{0,10:p}", freq); // το :p εμφανίζει σε ποσοστό %
            }
        } //τέλος traverseDlist

    } //τέλος class MyDoubleList

    public class MyDoublyDListApp // Driver Class, Main
    {

        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Started at: {0}\n", DateTime.Now);
                String file = @"C:\Users\GT780\Documents\Compunting\myProjects\myC#\RawDataTest.txt"; //το path του αρχείου
                StreamReader reader;
                reader = new StreamReader(file);
                MyDoubleList myDList = new MyDoubleList();
                DListNode pivot = null; // δείκτης στους κόμβους της λίστας
                if (!File.Exists(file)) //ελέγχει εάν υπάρχει το αρχείο 
                {
                    Console.WriteLine("ΤΟ ΑΡΧΕΙΟ ΔΕΝ ΒΡΕΘΗΚΕ");
                    Environment.Exit(-1);

                }

                char ch;
                float sumOfAllChars = 0; //αποθηκεύει το σύνολο όλων των χαρακτήρων

                while (reader.Peek() >= 0)
                {
                    ch = (char)reader.Read();
                    if (Convert.ToInt32(ch) != 13 && Convert.ToInt32(ch) != 10) // αν ο χαρακτήρα δεν είναι Carriage return(13) και Line feed(10) διάβασε τον αλλιώς κατανάλωσε τον
                    {
                        if (myDList.charListExists(ch, out pivot))
                        {
                            myDList.increaseCharcNumFound(ref pivot);
                        }
                        else myDList.insertFirst(ch);
                        sumOfAllChars++;

                    }
                    else reader.Read();
                }

                Console.WriteLine("ΤΑΞΙΝΟΜΗΣΗ ΜΕ ΒΑΣΗ ΤΟΥΣ ΧΑΡΑΚΤΗΡΕΣ:");
                myDList.shortListCharsCount();
                myDList.traverseDlist(sumOfAllChars);


                Console.WriteLine("\n\nΤΑΞΙΝΟΜΗΣΗ ΜΕ ΒΑΣΗ ΤΗΝ ΣΥΧΝΟΤΗΤΑ ΕΜΦΑΝΙΣΗΣ ΤΩΝ ΧΑΡΑΚΤΗΡΩΝ:");
                myDList.shortListIntCount();
                myDList.traverseDlist(sumOfAllChars);

                reader.Dispose();
                reader.Close();

                Console.WriteLine("\nEnded at: {0}\n", DateTime.Now);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());// εμφανίζει μήνυμα λάθους κατά την εκτέλεση του προγράμματος
            }

        } // τέλος main

    } //τέλος driver calss

} // τέλος namespace
