using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;

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

HttpRequestQuery httpRequestQuery = new HttpRequestQuery();

Observable
    .FromAsync(() => httpRequestQuery.Execute())
    .ValidateHttpRequestLenght(80)
    .Retry(3)
    .Catch(Observable.Return(new HttpRequestError() { Content = "Cats are cool." }))
    .Subscribe((httpRequestQuery) =>
    {
        Console.WriteLine(httpRequestQuery.Content);
    });


Console.ReadLine();

static class HttpRequestObservableExtensions
{
    public static IObservable<HttpRequest> ValidateHttpRequestLenght(this IObservable<HttpRequest> observable, int length)
    {
        return observable
            .Select(httpRequest =>
            {
                if (httpRequest.Content.Length > length)
                {
                    return Observable.Throw<HttpRequest>(new Exception("Http Request was too long"));
                }

                return Observable.Return(httpRequest);
            })
            .Switch();
    }
};