# Xamarin Forms ItemsControl

A simple implementation of the ItemsControl in Xamarin Forms.

If you don't know much about ItemsControl and what it's for, see the MSDN docs: https://msdn.microsoft.com/en-us/library/system.windows.controls.itemscontrol(v=vs.110).aspx

See video of this sample: https://www.youtube.com/watch?v=PMacCbmg51U

It's a simple control with little logic, it has an `ItemsSource` and `ItemTemplate` properties to create child items, but it delegates their positioning to a `Layout<View>` instance.
It does not use any renderers.

This allows displaying a bindable items source by any `Layout<View>` derived class. Xamarin Forms has some built-in layouts like `StackLayout`, `Grid`, `AbsoluteLayout`, `RelativeLayout`.
Or you can have your own layout to position items in a specific way. In this sample app to demo the control I have a `WrapLayout`.

By default, `ItemsControl` will use a `StackLayout` to position the items.

If the items layout template needs to be a more complicated hierarchy where the actual layout control is not the root, you need to set `IsItemsHost` property so that `ItemsControl` knows which is the actual layout where to add the items created by `ItemsSource` and `ItemTemplate`.

## Example

        <local:ItemsControl ItemsSource="{Binding Items}">
            <local:ItemsControl.ItemTemplate>
                <DataTemplate>
                    <Label BackgroundColor="Magenta"
                           Text="{Binding }"
                           WidthRequest="80"
                           HeightRequest="50" />
                </DataTemplate>
            </local:ItemsControl.ItemTemplate>
            <local:ItemsControl.ItemsLayout>
                <DataTemplate>
                    <ScrollView Orientation="Vertical">
                        <local:WrapLayout IsSquare="True"
                                          Spacing="10"
                                          local:LayoutEx.IsItemsHost="True" />
                    </ScrollView>
                </DataTemplate>
            </local:ItemsControl.ItemsLayout>
        </local:ItemsControl>
  
 
## Some things I'd like to share

1. It would make more sense if `ItemsControl` derived from Xamarin Form's built-in class `ItemsView` https://developer.xamarin.com/api/type/Xamarin.Forms.ItemsView%3CTVisual%3E/ since `ItemsView` already has `ItemsSource` and `ItemTemplate` properties.
I couldn't do this because `ItemsView` has an internal constructor! 

2. If you run the sample, you will see an issue with the demo main page. On Android, the `ItemsControl` overflows the lower StackPanel. This looks to be a problem because of `Grid` which still has issues in Xamarin Forms. 
On iOS it's worse, the `ItemsControl` completely takes the whole `Grid`. I put a BackgroundColor on the StackLayout created implicitly by the ItemsControl https://github.com/andreinitescu/XFItemsControl/blob/master/XFItemsControl/XFItemsControl/ItemsControl.cs#L55 and I could see how the items inside the StackLayout overflow it! I haven't tried it yet on a real iOS device but it doesn't look like a simulator issue.
