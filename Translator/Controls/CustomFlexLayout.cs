using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Translator.Controls
{
    public class CustomFlexLayout<T>:FlexLayout where T:class
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty
                .CreateAttached(nameof(ItemTemplateProperty), 
                                typeof(DataTemplate),
                                typeof(CustomFlexLayout<T>), 
                                default(DataTemplate));

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty
                .CreateAttached(nameof(ItemsSourceProperty),
                                typeof(IEnumerable<T>),
                                typeof(CustomFlexLayout<T>),
                                null,
                                BindingMode.OneWay,
                                null,
                                ItemsChanged);


        public IEnumerable<T> ItemsSource
        {
            get => (IEnumerable<T>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomFlexLayout<T>;
            if (control == null) return;

            control.Children.Clear();

            var items = (IEnumerable<T>) newValue;
            if (items.Any())
            {
                foreach (var item in items)
                    control.Children.Add(control.ViewFor(item));
            }
        }

        private View ViewFor(T item)
        {
            View view = null;

            if (ItemTemplate != null)
            {
                var content = ItemTemplate.CreateContent();
                view = (content is View) ? content as View : ((ViewCell)content).View;

                view.BindingContext = item;
            }

            return view;
        }
    }
}
