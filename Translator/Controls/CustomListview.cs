using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Translator.Controls
{
    class CustomListView: ListView
    {
        public static readonly BindableProperty SwipedItemProperty =
            BindableProperty.Create(
                propertyName: nameof(SwipedItem),
                returnType: typeof(object),
                declaringType: typeof(CustomListView),
                defaultBindingMode: BindingMode.OneWayToSource);


        public object SwipedItem
        {
            get
            {
                return this.GetValue(CustomListView.SwipedItemProperty);
            }
            set
            {
                this.SetValue(CustomListView.SwipedItemProperty, value);
            }
        }


        public void OnItemSelected(CustomViewCell selectedViewCell)
        {
            this.SelectedItem = selectedViewCell.BindingContext;
        }

        public void OnItemSwiped(CustomViewCell selectedViewCell)
        {
            this.SwipedItem = selectedViewCell.BindingContext;
        }
    }
}
