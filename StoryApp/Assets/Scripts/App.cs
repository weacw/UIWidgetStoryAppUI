using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;
using Image = Unity.UIWidgets.widgets.Image;
using Stack = Unity.UIWidgets.widgets.Stack;

public class App : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MaterialApp(
            home: new MyApp()
        );
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        FontManager.instance.addFont(Resources.Load<Font>("MaterialIcons-Regular"), "Material Icons");
    }
}


public class MyApp : StatefulWidget
{
    public override State createState()
    {
        return new _MyAppState();
    }
}

public class _MyAppState : State<MyApp>
{
    private float CurrentPage = ImageData.Images.Count - 1;
    private PageController Controller = new PageController();

    public override Widget build(BuildContext context)
    {
        Controller.addListener((() => setState(() => { CurrentPage = Controller.page; })));
        return new Scaffold(
            backgroundColor: new Color(0xff2d3447),
            body: new SingleChildScrollView(
                child: new Column(
                    children: new List<Widget>
                    {
                        new Padding(
                            padding: EdgeInsets.only(left: 12, right: 12, top: 30, bottom: 8),
                            child: new Row(
                                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                children: new List<Widget>
                                {
                                    new IconButton(icon: new Icon(icon: Icons.menu, color: Colors.white, size: 30f)),
                                    new IconButton(icon: new Icon(icon: Icons.search, color: Colors.white, size: 30f))
                                }
                            )),
                        new Padding(
                            padding: EdgeInsets.symmetric(horizontal: 20f),
                            child: new Row(
                                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                children: new List<Widget>
                                {
                                    new Text("Trending", style: new TextStyle(color: Colors.white,
                                        fontSize: 46,
                                        letterSpacing: 1f
                                    )),
                                    new IconButton(icon: new Icon(icon: Icons.laptop, size: 12, color: Colors.white))
                                }
                            )
                        ),

                        new Padding(
                            padding: EdgeInsets.symmetric(horizontal: 20f),
                            child: new Row(
                                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                children: new List<Widget>
                                {
                                    new Container(
                                        decoration: new BoxDecoration(
                                            color: new Color(0xffff6e6e),
                                            borderRadius: BorderRadius.circular(20f)
                                        ),
                                        child: new Center(
                                            child: new Padding(
                                                padding: EdgeInsets.symmetric(horizontal: 22f, vertical: 6f),
                                                child: new Text("Animated", style: new TextStyle(color: Colors.white))
                                            )
                                        )
                                    ),
                                    new SizedBox(width: 15),
                                    new Text("25+ Stories", style: new TextStyle(color: Colors.blueAccent))
                                }
                            )
                        ),
                        new Stack(
                            children: new List<Widget>
                            {
                                new CardScrollWidget(CurrentPage),
                                Positioned.fill(
                                    child: new PageView(
                                        controller: Controller,
                                        itemCount: ImageData.Images.Count,
                                        reverse: true,
                                        itemBuilder: ((buildContext, index) => new Container())
                                    )
                                )
                            }
                        ),
                        new Padding(
                            padding: EdgeInsets.symmetric(horizontal: 20.0f),
                            child: new Row(
                                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                children: new List<Widget>
                                {
                                    new Text("Favourite",
                                        style: new TextStyle(
                                            color: Colors.white,
                                            fontSize: 46.0f,
                                            letterSpacing: 1.0f
                                        )),
                                    new IconButton(
                                        icon: new Icon(
                                            Icons.poll,
                                            size: 12.0f,
                                            color: Colors.white
                                        )
                                    )
                                }
                            )
                        ),
                        new Padding(
                            padding: EdgeInsets.only(left: 20.0f),
                            child: new Row(
                                children: new List<Widget>
                                {
                                    new Container(
                                        decoration: new BoxDecoration(
                                            color: Colors.blueAccent,
                                            borderRadius: BorderRadius.circular(20f)
                                        ),
                                        child: new Center(
                                            child: new Padding(
                                                padding: EdgeInsets.symmetric(horizontal: 22f, vertical: 6f),
                                                child: new Text("Latest",
                                                    style: new TextStyle(color: Colors.white))
                                            )
                                        )
                                    ),
                                    new SizedBox(width: 15f),
                                    new Text("9+ Stories", style: new TextStyle(color: Colors.blueAccent))
                                }
                            )
                        ),
                        new SizedBox(height: 20f),
                        new Row(
                            children: new List<Widget>
                            {
                                new Padding(
                                    padding: EdgeInsets.only(left: 18.0f),
                                    child: new ClipRRect(
                                        borderRadius: BorderRadius.circular(20f),
                                        child: Image.network("assets/image_02.jpg", width: 296.0f,
                                            height: 222.0f)
                                    )
                                )
                            }
                        )
                    }
                )
            )
        );
    }
}


public class CardScrollWidget : StatelessWidget
{
    private float CurrentPage;
    private static float CardAspectRatio = 12f / 16f;
    private static float WidgetAspectRatio = CardAspectRatio * 1.2f;
    private float Padding = 20f;
    private float VerticalInset = 20f;
    List<Widget> CardList = new List<Widget>();

    public CardScrollWidget(float currentPage)
    {
        CurrentPage = currentPage;
        Debug.Log(CardAspectRatio * 1.2f);
    }

    public override Widget build(BuildContext context)
    {
        return new AspectRatio(
            aspectRatio: WidgetAspectRatio,
            child: new LayoutBuilder(builder: (buildContext, constraints) =>
                {
                    var width = constraints.maxWidth;
                    var height = constraints.maxHeight;


                    var safeWidth = width - 2 * Padding;
                    var safeHeight = height - 2 * Padding;

                    var heightOfPrimaryCard = safeHeight;
                    var widthOfPrimaryCard = heightOfPrimaryCard * CardAspectRatio;


                    var primaryCardLeft = safeWidth - widthOfPrimaryCard;
                    var horizontalInset = primaryCardLeft / 2;
                    var boxShadow = new BoxShadow(color: Colors.black12, offset: new Offset(3.0f, 6.0f),
                        blurRadius: 10.0f);
                    for (int i = 0; i < ImageData.Images.Count; i++)
                    {
                        var tmpDelta = i - CurrentPage;
                        bool isOnRight = tmpDelta > 0;

                        var Start = Padding + Mathf.Max(
                                        primaryCardLeft - horizontalInset * -tmpDelta * (isOnRight ? 15 : 1),
                                        0f);

                        var cardItem = Positioned.directional(
                            top: Padding + VerticalInset * Mathf.Max(-tmpDelta, 0f),
                            bottom: Padding + VerticalInset * Mathf.Max(-tmpDelta, 0f),
                            start: Start,
                            textDirection: TextDirection.rtl,
                            child: new ClipRRect(
                                borderRadius: BorderRadius.circular(16.0f),
                                child: new Container(
                                    decoration: new BoxDecoration(color: Colors.white,
                                        boxShadow: new List<BoxShadow>() {boxShadow}),
                                    child: new AspectRatio(
                                        aspectRatio: CardAspectRatio,
                                        child: new Stack(
                                            fit: StackFit.expand,
                                            children: new List<Widget>
                                            {
                                                Image.network(ImageData.Images[i], fit: BoxFit.cover),
                                                new Align(
                                                    alignment: Alignment.bottomLeft,
                                                    child: new Column(
                                                        mainAxisSize: MainAxisSize.min,
                                                        crossAxisAlignment: CrossAxisAlignment.start,
                                                        children: new List<Widget>
                                                        {
                                                            new Padding(
                                                                padding: EdgeInsets.symmetric(horizontal: 16.0f,
                                                                    vertical: 8.0f),
                                                                child: new Text(ImageData.Titles[i],
                                                                    style: new TextStyle(color: Colors.white,
                                                                        fontSize: 25.0f))
                                                            ),
                                                            new SizedBox(height: 10.0f),
                                                            new Padding(
                                                                padding: EdgeInsets.only(left: 12.0f, bottom: 12.0f),
                                                                child: new Container(
                                                                    padding: EdgeInsets.symmetric(horizontal: 22.0f,
                                                                        vertical: 6.0f),
                                                                    decoration: new BoxDecoration(
                                                                        color: Colors.blueAccent,
                                                                        borderRadius: BorderRadius.circular(20.0f)),
                                                                    child: new Text("Read Later",
                                                                        style: new TextStyle(color: Colors.white))
                                                                )
                                                            )
                                                        }
                                                    )
                                                )
                                            }
                                        )
                                    )
                                )
                            )
                        );
                        CardList.Add(cardItem);
                    }

                    return new Stack(children: CardList);
                }
            )
        );
    }
}