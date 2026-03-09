using System.Diagnostics;
using System.Xml.Linq;

class Activitate
{
    //instantele clasei
    
    private string name;
    private string description;
    private string type;

    //definirea metodelor
    public Activitate() // Constructorul implicit ai clasei
    {
        name = null;
        description = null;
        type = null;
    }
    public Activitate(string _name, string _description, string _tip) // Constructorul ai clasei
    {
        name = _name;
        description = _description;
        type = _tip;
    }

    public void Set_name(string _name) // metoda de setare/schimbare a numelui activitatii
    {
        this.name = _name;
    }
    public void Set_description(string _description) // metoda de setare/schimbare a descrierii activitatii
    {
        this.description = _description;
    }
    public void Set_tip(string _tip) // metoda de setare/schimbare a tipului activitatii
    {
        this.type = _tip;
    }
    //Metodele de returnare a datelor membre ai clasei
    public string GetName() // returneaza numele activitatii
    {
        return this.name;
    }
    public string GetDescription() // returneaza descrierea activitatii
    {
        return this.description;
    }
    public string GetTip() // returneaza tipul activitatii
    {
        return this.type;
    }
}

class Calendar
{
    private List<Activitate> activities;
    private string[,] calendar;
    private int nrOre;
    public static string defaultfill = "---";
    public static string[] zile = {"LUNI", "MARTI", "MIERCURI", "JOI", "VINERI", "SAMBATA", "DUMINICA" };

    public Calendar(int _nrOre)
    {
        nrOre = _nrOre;  //setam pasul de ore custom va fi ales dintre 15min, 30min si 1h
        calendar = new string[nrOre,7];

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < nrOre; j++)
            {
                calendar[j, i] = defaultfill;
            }
        }
    }
    public Calendar()
    {
        nrOre = 48; // setam pasul de ore default pe 30 de min
        calendar = new string[nrOre, 7];
        
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < nrOre; j++)
            {
                calendar[j, i] = defaultfill;
            }
        }
    }

    public bool inList(Activitate activity) // spune daca activitatea exista in lista de activitati sau nu
    {
        return activities.Contains(activity);
    }
    public void add_activityinlist(Activitate activity) // adauga activitate in lista de activitati
    {
        if (!this.inList(activity)) {
            this.activities.Add(activity);
        }
    }
    public void remove_by_name(string activity_name) // sterge activitatea dupa nume din lista de activitati
    {
        if (activities.Count != 0) //controlam daca lista nu e goala
        { 
            foreach (Activitate activity in activities) // daca lista nu este goala trecem prin fiecare activitate din lista activities
            {
                if(activity.GetName() == activity_name) // comparam numele activitati din lista cu numele de activitate pe care o cautam
                {
                    activities.Remove(activity); // daca numele activitati din lista este acelasi cu numele activitatii pe care o cautam o scoatem pe ea din lista de activitati
                    break; // terminam bucla
                }
            }
        }
    }
    
    public int FindActivitybyName(string activity_name)
    {
        if (activities.Count != 0) //controlam daca lista nu e goala
        {
            foreach (Activitate activity in activities) // daca lista nu este goala trecem prin fiecare activitate din lista activities
            {
                if (activity.GetName() == activity_name) // comparam numele activitati din lista cu numele de activitate pe care o cautam
                {
                    return activities.IndexOf(activity); // returnam indexul activitatii
                }
            }
        }
        return -1; //daca activiatea nu s-a gasit returnam -1
    }

    public void AddActivityinCalendar(Activitate activity, float start_time, float stop_time, int zi)
    {
        int pos_start, pos_stop;
        int pas_ora = Convert.ToInt32((24.0f / nrOre) * 60);
        int startminute = Convert.ToInt32(start_time) * 60 + Convert.ToInt32((start_time % 1) * 100);
        int stopminute = Convert.ToInt32(stop_time) * 60 + Convert.ToInt32((stop_time % 1) * 100);
        pos_start = (startminute / pas_ora);
        pos_stop = (stopminute / pas_ora);

        for (int i = pos_start; i < pos_stop+1; i++)
        {
            this.calendar[i, zi] = activity.GetName();
        }
    }

    public string showALL()
    {
        string calendar_str="";
        string linie = "";   
        int ora, minuta;
        int pas_ora = Convert.ToInt32( (24.0f / nrOre) * 60 );
        for (int i = 0; i< 7; i++)
        {
            
            linie = "";
            for (int j = 0; j < this.nrOre; j++)
            {
                
                ora = (j * pas_ora) / 60;
                minuta = (j * pas_ora) % 60;
                linie += $"{ora:D2}:{minuta:D2} ";
                linie += this.calendar[j, i] + '\n';
                
            }
            calendar_str += $"----------{zile[i]}----------\n";
            calendar_str += linie;
        }
        
        return calendar_str;
    }

}

public class ProjectPIU
{
    public static void Main(string[] args)
    {
        Calendar c1 = new Calendar();
        Activitate a1 = new Activitate("Scoala", "Activitatea de studiu la USV", "Invatamant");

        c1.AddActivityinCalendar(a1, 8.15f, 14.30f, 0);
        c1.AddActivityinCalendar(a1, 10.45f, 16f, 1);
        c1.AddActivityinCalendar(a1, 13.15f, 18.20f, 2);
        c1.AddActivityinCalendar(a1, 10f, 17.30f, 3);

        Console.WriteLine(c1.showALL());

        Console.ReadKey();
    }
}
