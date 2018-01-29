# Xamarin Forms ItemsControl

A (very) naive tentative to implement an ItemsControl.
If you don't know much about ItemsControl and its usefulness, read MSDN docs: https://msdn.microsoft.com/en-us/library/system.windows.controls.itemscontrol(v=vs.110).aspx

It's a simple control, it has an `ItemsSource` and `ItemTemplate` properties to create child items, but it delegates their positioning to a `Layout<View>` instance.
It does not use any renderers.

This allows displaying a bindable items source by any `Layout<View>` derived class. Xamarin Forms has some built-in layouts like `StackLayout`, `Grid`, `AbsoluteLayout`, `RelativeLayout`.
Or you can have your own layout to position items in a specific way. In this sample app to demo the control I have a `WrapLayout`.

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
  
  
  ## Discussion
  
  
  
