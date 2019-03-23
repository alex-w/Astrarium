﻿using ADK;
using Planetarium.Objects;
using Planetarium.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Planetarium.Views
{
    public class CelestialObjectPicker : Control
    {
        public CelestialObjectPicker() { }

        public CelestialObject SelectedBody
        {
            get { return (CelestialObject)GetValue(SelectedBodyProperty); }
            set { SetValue(SelectedBodyProperty, value); }
        }
        public readonly static DependencyProperty SelectedBodyProperty = DependencyProperty.Register(
            nameof(SelectedBody), 
            typeof(CelestialObject), 
            typeof(CelestialObjectPicker), 
            new FrameworkPropertyMetadata(null, (o, e) =>
            {
                CelestialObjectPicker picker = (CelestialObjectPicker)o;
                picker.RaiseSelectedBodyChangedEvent(e);
            })
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public event EventHandler<DependencyPropertyChangedEventArgs> SelectedBodyChanged;
        private void RaiseSelectedBodyChangedEvent(DependencyPropertyChangedEventArgs e)
        {
            SelectedBodyChanged?.Invoke(this, e);
        }

        public ISearcher Searcher
        {
            get { return (ISearcher)GetValue(SearcherProperty); }
            set { SetValue(SearcherProperty, value); }
        }

        public readonly static DependencyProperty SearcherProperty = DependencyProperty.Register(
            nameof(Searcher), 
            typeof(ISearcher), 
            typeof(CelestialObjectPicker));

        public string SelectedBodyName
        {
            get { return (string)GetValue(SelectedBodyNameProperty); }
            private set { SetValue(SelectedBodyNameProperty, value); }
        }
        public readonly static DependencyProperty SelectedBodyNameProperty = DependencyProperty.Register(
            nameof(SelectedBodyName), 
            typeof(string), 
            typeof(CelestialObjectPicker));

        public IViewManager ViewManager
        {
            get { return (IViewManager)GetValue(ViewManagerProperty); }
            set { SetValue(ViewManagerProperty, value); }
        }
        public readonly static DependencyProperty ViewManagerProperty = DependencyProperty.Register(
            nameof(ViewManager), 
            typeof(IViewManager), 
            typeof(CelestialObjectPicker), 
            new UIPropertyMetadata(null));

        TextBox _TextBox;
        Button _Button;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _TextBox = Template.FindName("TextBox", this) as TextBox;
            _Button = Template.FindName("Button", this) as Button;
            _Button.Click += ShowSearchWindow;
        }

        private void ShowSearchWindow(object sender, RoutedEventArgs e)
        {
            var vm = ViewManager.CreateViewModel<SearchVM>();
            if (ViewManager.ShowDialog(vm) ?? false)
            {
                SelectedBody = vm.SelectedItem.Body;
                SelectedBodyName = vm.SelectedItem.Name;
            }
        }
    }
}
