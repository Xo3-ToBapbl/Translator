using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Translator.Controls
{
    public class CustomFlexLayout<T>:FlexLayout where T:class
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty
                .CreateAttached(propertyName: nameof(ItemTemplateProperty),
                                returnType: typeof(DataTemplate),
                                declaringType: typeof(CustomFlexLayout<T>),
                                defaultValue: default(DataTemplate));

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty
                .CreateAttached(propertyName:nameof(ItemsSourceProperty),
                                returnType: typeof(IEnumerable<T>),
                                declaringType: typeof(CustomFlexLayout<T>),
                                defaultValue:null,
                                defaultBindingMode: BindingMode.OneWay,
                                propertyChanged: ItemsChanged);


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
            if (bindable is CustomFlexLayout<T> control)
            {
                control.UpdateChildren();
                if (newValue is INotifyCollectionChanged observableCollection)
                {
                    observableCollection.CollectionChanged += (sender, e) =>
                    {
                        control.UpdateChildren();
                    };
                }
            }
        }

        private void UpdateChildren()
        {
            this.Children.Clear();

            var items = ItemsSource;
            if (items?.Any() ?? false)
            {
                foreach (var item in items)
                    this.Children.Add(this.ViewFor(item));
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
