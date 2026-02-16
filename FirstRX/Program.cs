using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Reactive.Subjects;

//void Countdown(int seconds)
//{
//    Observable
//        .Timer(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1))
//        .Select(currentSeconds => seconds - currentSeconds)
//        .TakeWhile(currentSeconds => currentSeconds > 0)
//        .Subscribe((currentSeconds) =>
//        {
//            Console.WriteLine(currentSeconds);
//        },
//        () =>
//        {
//            Console.WriteLine("Blast off!");
//        });
//}

//Countdown(5);

//HttpRequestQuery httpRequestQuery = new HttpRequestQuery();

//Observable
//    .FromAsync(() => httpRequestQuery.Execute())
//    .ValidateHttpRequestLenght(80)
//    .Retry(3)
//    .Catch(Observable.Return(new HttpRequestError() { Content = "Cats are cool." }))
//    .Subscribe((httpRequestQuery) =>
//    {
//        Console.WriteLine(httpRequestQuery.Content);
//    });


//Console.ReadLine();

//static class HttpRequestObservableExtensions
//{
//    public static IObservable<HttpRequest> ValidateHttpRequestLenght(this IObservable<HttpRequest> observable, int length)
//    {
//        return observable
//            .Select(httpRequest =>
//            {
//                if (httpRequest.Content.Length > length)
//                {
//                    return Observable.Throw<HttpRequest>(new Exception("Http Request was too long"));
//                }

//                return Observable.Return(httpRequest);
//            })
//            .Switch();
//    }
//};

//Observer pattern RX.NET

using Observable observable = new();
Observer observer = new();
observable.Subscribe(observer);

observable.Publish(new("Hello, World!"));
observable.Publish(new("Goodbye, World!"));

public sealed record Message(string body);


public class Observer : IObserver<Message>
{
    public void OnCompleted()
    {
        Console.WriteLine($"OnCompleted fired");
    }

    public void OnError(Exception error)
    {

    }

    public void OnNext(Message value)
    {
        Console.WriteLine($"OnNext fired: {value.body}");
    }
}

public class Observable : IObservable<Message>, IDisposable
{
    private readonly Subject<Message> _subject;

    public Observable()
    {
        _subject = new Subject<Message>();
    }

    public void Dispose()
    {
        _subject.OnCompleted();
        _subject.Dispose();
    }

    public IDisposable Subscribe(IObserver<Message> observer)
    {
        return _subject.Subscribe(observer);  
    }

    public void Publish(Message value)
    {
        _subject.OnNext(value);
    }
}