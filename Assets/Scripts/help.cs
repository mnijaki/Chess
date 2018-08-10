// ***************************************************************************************
//                                         INFO
// # 'set' i 'get' to akcesory. Zezwlają one na pobieranie oraz nadpisywanie pól 
//   prywatnych klasy. Jest to mniej więcej równowartość poniższego zapisu. 
//   'private int wiek; public int WiekGet()... public WiekSet(int value)...' Przykład:
//      abstract class BaseClass   // Abstract class
//      {
//        protected int _x = 100;
//        protected int _y = 150;
//        public abstract int X { get; }
//        public abstract int Y { get; }
//      }
//
// # Pola statyczne (static fields eg: 'public static int x'):
//   - dostępne nawet przed inicjalizacją obiektu klasy, w której są zdefiniowane
//   - dostęp do pola statycznego odbywa się zawsze przez nazwę klasy + nazwę
//     pola. Nigdy przez nazwę obiektu+nazwa pola!!!
//   - niezależnie od ilości utworzonych obiektów klasy zawsze istnieje tylko jedna
//     kopia statycznego pola
//   - pola statyczne nie mają dostępu do pól oraz zdarzeń niestatycznych
//   - najczęstsze zastosowanie: zliczanie ilośći wystąpień obiektu danego typu 
//     lub przechowywanie wartości, która ma być dostępna przez wszystkie obiekty
//     tego typu
//
// # Pola 'const'
//   - variables flagged as 'const' cannot be changed ever after compile time. They are good for things that are 
//     truly constant for egzample 'Pi'
//   - A constant member is defined at compile time and cannot be changed at runtime. Constants are declared as a field, 
//     using the const keyword and must be initialized as they are declared.
//
// # MonoBehaviour:
//   - skrypty, które mają być podpięte pod obiekty gry powinny dziedziczyć z tej klasy
//   - zezwala ona na wiele rzczy związanych z obiektami gry, do których nie mielibyśmy
//     dostępu w zwykłej klasie
//
// # W C# wszystkie typy są tak naprawdę poliformiczne ponieważ wszystkie dziedziczą z klasy 'Object'.
//   Dodatkowo w C# klasa może dziedziczyć TYLKO z jednej klasy. Aby zrobić coś na wzór dziedziczenia
//   z wielu klas należy skorzystać z intefejsów.
//
// # W C# jeżeli chcemy używać jakieś zmiennej to musimy ją albo zainicjalizować lub przypisać jej wartość 'null'. Np.:
//     int x=null;
//     if(x==null) ...
//
// # Dostępność:
//      public - The type or member can be accessed by any other code in the same assembly or another assembly that references it.
//      private - The type or member can only be accessed by code in the same class or struct.
//      protected - The type or member can only be accessed by code in the same class or struct, or in a derived class.
//      internal - The type or member can be accessed by any code in the same assembly, but not from another assembly.
//      protected internal - The type or member can be accessed by any code in the same assembly, or by any derived class in another assembly.
//
// # Rzutowanie
//   ((Base) d).DoWork(val);  // Calls DoWork(int) on Derived
//
// # Using/namespace:
//    using System.Collections.Generic;  // Contains interfaces and classes that define generic collections, which allow users to create 
//                                          strongly typed collections that provide better type safety and performance.
//
// # Konstruktory, abstrakcyjność, wirtualność, interfejsy:
//    - Klasa abstrakcyjna:
//        abstract class Shape
//        {
//          public const double pi = Math.PI;
//          protected double radius, x, y;
//
//          public Shape(double radius, double x,double y)
//          {
//            this.radius=radius;
//            this.x=x;
//            this.y=y;
//          }
//
//          public virtual double Area();
//        }
//
//    - Klasa posiadająca konstruktor wyołujący konstruktor z klasy nadrzędnej/bazowej. 
//      Ponadto klasa ta nadpisuje metodę wirtualną 'Area':
//        class Circle : Shape
//        {
//          public Circle(double radius, double height) : base(radius,0,0)
//          {
//            this.y=height*2;
//          }
//
//          public override double Area()
//          {
//            return pi*x*x;
//          }
//        }
//
//    - Metody które mają być nadpisywane przez klasy potomne powinny być zdefiniowane jako
//      'virtual' albo 'abstact'. Różnica polega na tym, że 'virtual' nie może być stosowane
//      do statycznych (static) pól/metod. Słowo kluczowe 'abstract' mówi ponadto, że dana 
//      implementacja jest niepełna i potrzebuje uzupełnienia w klasach dziedziczących. 
//      Z tego powodu klasa abstrakcyjna nie może być przez to zainicjalizowana. 
//      Analogicznie z metodą abstakcyjną (po jej deklaracji pisze się poprostu średnik). 
//      Metody abstrakcyjne są dostępne TYLKO w klasach abstrakcyjnych.
//      Klasą silnie abstakcyjną jest interfejs. Wszytskie pola interfejsu NIE MOGĄ mieć
//      implementacji (w odróżnieniu do klasy abstakcyjnej). Dodatkowo interfejsy zezwalają coś
//      na wzór dziedziczenia dla struktur (struktury w C# nie mogą dziedziczyć). Ponadto
//      interfejsy zezwalają coś na wzór dziedziczenia klasy z wilu klas (w C# klasa może dziedziczyć
//      TYLKO z jednej klasy).
//      https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface
//      https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/index
//      Przykład interfejsu:
//        class ShapesTest 
//        {
//          static void Main()
//          {
//            AdvancedShape as = new AdvancedShape();  // Creating instance of 'AdvancedShape' class.
//            IRadius rad=(IRadius)as;                 // Creating instance of 'AdvancedShape' class with 'IRadius' interfece.
//            IColor col=(IColor)as;                   // Creating instance of 'AdvancedShape' class with 'IColor' interfece.
//            // Painting.
//            as.Paint();                              // Error
//            rad.Paint();                             // Calls IRadius.Paint on AdvancedShape.
//            col.Paint();                             // Calls IColor.Paint on AdvancedShape.
//          }
//        }
//
//        interface IRadius
//        {
//          int P { get;}
//          void Paint();
//        }
//
//        interface IColor
//        {
//          int P();
//          void Paint();
//        }
// 
//        class AdvancedShape : IControl, ISurface
//        {
//          int IRadius.P { get { return 0; } }
//          public void IRadius.Paint()
//          {
//            Console.WriteLine("Paint method in IRadius interface");
//          }
//
//          public int IColor.P() { return 0; }
//          public void IColor.Paint()
//          {
//            Console.WriteLine("Paint method in IColor interface");
//          }
//        }
//
//    - Metoda abstrakcyjna/wirtualna jest wywoływana nawet gdy instancja (obiekt) danej klasy jest zdefiniowana
//      jako instancja (obiekt) klasy podsawowej (bazowej), np.:
//        Circle C = new Circle();        // Create an instance of class 'Circle'
//        C.Area();                       // Calls overriden method from 'Circle'.
//        Shape C2 = (Shape)C;            // Cast 'Circle' instance as 'Shape' instance.
//        C2.Area();                      // Also calls overriden method 'Circle'.
//
//    - Ukrywanie metod klasy bazowej w klasie podstawowej używamy, gdy chcemy zdefiniować jakąś metodę 
//      o takiej samej nazwie jak metoda z klasy bazowej, ale nie chcemy by uczestniczyła ona w wirtualnym/abstrakcyjnym wywołaniu.
//      W poniższym przykładzie zmieniła się tylko implementacja klasy 'Circle', klasa 'Shape' pozostaje bez zmian.
//        class Circle : Shape
//        {
//          public Circle(double radius, double height) : base(radius,0,0)
//          {
//            y = height;
//          }
//
//          public new double Area()
//          {
//            return pi*x*x;
//          }
//        }
//
//        Circle C = new Circle();        // Create an instance of class 'Circle'
//        C.Area();                       // Calls method from 'Circle'.
//        Shape C2 = (Shape)C;            // Cast 'Circle' instance as 'Shape' instance.
//        C2.Area();                      // Calls method from 'Shape'.
//
//    - Zabronienie/przerwanie łańcucha wirtualnych wywołań można osiągnąć poprzez słowo kluczowe 'sealed'. 
//      W poniższym przykładzie klasa 'SmallCircle' 'zamyka' wirtualne wywołania metody 'Area'.
//      Wszystkie obiekty potomne dla klasy 'SmallCircle' nie mogą już uczestniczyć w wirtualnym wywołaniu metody 'Area',
//      natomiast obiekty klasy 'SmallCircle' nadal mogą. Klasy potomne mogą mieć metodę 'Area' ale nie będzie jej można nadpisać, 
//      jedynie stworzyć jej całkowicie nową implementację poprzez słowo kluczowe ':new'. Przykład:
//        class SmallCircle : Circle
//        {
//          public SmallCircle(double radius, double height) : base(radius,0,0)
//          {
//            y = height;
//          }
//
//          public sealed override Area()
//          {
//            return pi*x*x;
//          }
//        }
//  
//        class VerySmallCircle : SmallCircle
//        {
//          public VerySmallCircle(double radius, double height) : base(radius,0,0)
//          {
//            y = height;
//          }
//
//          public new Area()
//          {
//            return pi*x*x;
//          }
//        }
//        VerySmallCircle C = new VerySmallCircle();        // Create an instance of class 'VerySmallCircle'
//        C.Area();                                         // Calls method from 'VerySmallCircle'.
//        Shape C2 = (Shape)C;                              // Cast 'VerySmallCircle' instance as 'Shape' instance.
//        C2.Area();                                        // Calls method from 'Shape'.
//
//    - Klasy zamknięte definiujemy poprzez słowo kluczowe 'sealed'. Z klas zamknietych nie można
//      dziedziczyć dlatego też nie mogą być one klasami abstrakcyjnymi.
//
//    - Metody z klasy dziedziczącej nadpisujące metody wirtualne z klasy bazowej powinny zawsze 
//      uruchamiać metody z klasy bazowej. Robimy tak po to by w klasie dziedziczącej pisać tylko 
//      zmodyfikowane zachowanie a podstawowe pochodziło zawsze z klasy bazowej. 
//      Odwołania do bazowych składowych jest dostępne tylko dla:  konstruktorów,  metod oraz akcesorów.
//      Odwołań bazowych nie można używać dla metod statycznych. 
//      Przykład:
//        class Circle : Shape
//        {
//          public Circle(double radius, double height) : base(radius,0,0)
//          {
//            y = height;
//          }
//          public Circle(double radius, double height, double width) : base(radius,0,0)
//          {
//            y = height;
//            x = width;
//          }
//
//          public virtual Area()
//          {          
//            return pi*x*x;
//          }
//        }
// 
//        class SmallCircle : Circle
//        {
//          public SmallCircle(double radius, double height) : base(radius,height)
//          {
//          }
//          public SmallCircle(double radius, double height, double width) : base(radius,height,width)
//          {
//          }
//
//          public override Area()
//          {
//            return base.Area();
//          }
//        }
//
//  # Indeksery:
//    - służą do łatwiejszego odwoływania się do pól klasy, będących tablicami. Przykład
//        Using a string as an indexer value
//        class DayCollection
//        {
//          string[] days = { "Sun", "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat" };
//
//          // This method finds the day or returns -1
//          private int GetDay(string testDay)
//          {
//            for (int j = 0; j < days.Length; j++)
//            {
//              if (days[j] == testDay)
//              {
//                return j;
//              }
//            }
//
//            throw new System.ArgumentOutOfRangeException(testDay, "testDay must be in the form \"Sun\", \"Mon\", etc");
//          }
//
//          // The get accessor returns an integer for a given string
//          public int this[string day]
//          {
//            get
//            {
//              return (GetDay(day));
//            }
//          }
//        }
//      }
//
//      class Program
//      {
//        static void Main(string[] args)
//        {
//          DayCollection week = new DayCollection();
//          System.Console.WriteLine(week["Fri"]);              // Will give output: 5
//
//          // Raises ArgumentOutOfRangeException
//          System.Console.WriteLine(week["Made-up Day"]);
//
//          // Keep the console window open in debug mode.
//          System.Console.WriteLine("Press any key to exit.");
//          System.Console.ReadKey();
//        }
//      }
//
// # Corutines + move animation
/*
// Move animation.
private void MoveAnim(Vector3 target_pos,float degree)
{
  // Move start time.
  float start_time = Time.time;
  // Animate move.
  StartCoroutine(MoveAnimStep(start_time,target_pos,degree));
} // End MoveAnim


// Move animation step.
public IEnumerator MoveAnimStep(float start_time,Vector3 target_pos,float degree)
{
  // Loop until piece reach targeted position.
  while(this.transform.position!=target_pos)
  {
    // Calculate time for interpolation.
    float t = (Time.time-start_time)/this.move_anim_speed;
    // Move piece.
    this.transform.position=new Vector3(Mathf.SmoothStep(this.transform.position.x,target_pos.x,t),
                                        Mathf.SmoothStep(this.transform.position.y,target_pos.y,t),
                                        Mathf.SmoothStep(this.transform.position.z,target_pos.z,t));

    //this.transform.rotation=Quaternion.Slerp(this.transform.rotation,new Quaternion(0,0,0,Mathf.SmoothStep(this.transform.rotation.z,40,t)),t);
    Quaternion q = this.transform.rotation;
    //q.to
    //this.transform.Rotate(q.x,q.y,Mathf.SmoothStep(q.z,40,t),Space.World);

    if(q.z!=40)
    {
      float xx = Mathf.SmoothStep(q.z,(this.Is_white ? -40 : 40),t);
      //this.transform.Rotate(xx,0,0,Space.World);
      this.transform.Rotate(0,0,(this.Is_white ? -10 : 10));
    }

    //this.transform.RotateAround(this.transform.position,new Vector3(1f,0f,0f),degree/t);
    // Return to coroutine.
    yield return null;
  }
} // End of MoveAnim
*/
// 
// # Extenions method ???
//    https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
//
// # Laambda expressions:
//     https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions
//
// # Finalizers:
//     https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/destructors
//
// # Static constructors:
//     https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-constructors
//
// # Events:
//     https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/event
//
// # Generics:
//     https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
// # Unity constructors and serialization/deserialization:
//     https://blogs.unity3d.com/2016/06/06/serialization-monobehaviour-constructors-and-unity-5-4/
//     https://answers.unity.com/questions/232531/class-constructor.html
//     https://answers.unity.com/questions/32413/using-constructors-in-unity-c.html
//     https://answers.unity.com/questions/862032/c-constructor-in-monobehaviour.html
//     https://unity3d.com/learn/tutorials/topics/scripting/serialization-and-game-data
//
// # Wzorzec Strategy?
//     https://en.wikipedia.org/wiki/Strategy_pattern
//
// # DESTROY
//  Destroy(gameObject) is the correct way to the GameObject your script is attached to.
//  GameObject is a class.
//  gameObject is a property you can call on a MonoBehaviour to the the GameObject instance that Monobehaviour instance is attached to.
//  this is the instance of the class the code is in.
//  Destroy(this) will not destroy GameObject but only destroy the script of GameObject component
//
//
//Debug.Log(System.Enviroment.Version);???
//
// Do not confuse the concept of passing by reference with the concept of reference types. The two concepts are not the same??? 'ref'
// 
// unity nie wspiera readonly (poniewaz zmiennej z o takiej charakterystyce można przypisać wartość tylko podczas deklarowania,
// albo w konstruktorze, a że konstruktory parametrowe nie sa dostepne z tego powodu nie ma wsparcia dla readonly). Obejscie tego
// znajduje sie w ponizszym przykładzie:
//   Information if chess piece is white.
//   private bool _is_white;
//   public bool Is_white
//   {
//     get
//     {
//        return this._is_white;
//     }
//   }
//   Function that set the position of chess piece.
//   public void PosSet(int x,int z)
//   {
//     this._x=x;
//     this._z=z;
//   } // End of PosSet

// down vote
// The reference is passed by value.

// Arrays in .NET are object on the heap, so you have a reference.That reference is passed by value,meaning that changes to the contents of the array will be seen by the caller,but reassigning the array won't:

// void Foo(int[] data) {
// data[0]=1; // caller sees this
// }
// void Bar(int[] data)
// {
// data=new int[20]; // but not this
// }
// If you add the ref modifier, the reference is passed by reference - and the caller would see either change above.

// jak jest z dyrejtywami using? sa one dziedzicznoe? (np System z Piece.cs)


// dynamic?

// finalize?

// static cast?

// przekazywanie zmiennych stalych itp (ref itp)


/*
// Wait function.
  private void WaitSec(float seconds)
  {
    DateTime t1 = System.DateTime.Now;    
    while(System.DateTime.Now.Subtract(t1).Seconds<seconds)
    {
      
    }
  } // End Of WaitSec
  */


