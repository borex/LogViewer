﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogRecordsTableViewModel.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LogViewer.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Data;

    using Catel;
    using Catel.Collections;
    using Catel.MVVM;

    using LogViewer.Models;
    using LogViewer.Models.Base;

    public class LogRecordsTableViewModel : ViewModelBase
    {
        public LogRecordsTableViewModel(LogViewerModel logViewerModel)
        {
            LogViewer = logViewerModel;
            LogRecords = new LogRecordsCollection(new ObservableCollection<LogRecord>());
        }

        [Model]
        public LogViewerModel LogViewer { get; set; }

        [ViewModelToModel("LogViewer")]
        public NavigationNode SelectedItem { get; set; }

        public CollectionView LogRecords { get; set; }


        public void OnSelectedItemChanged()
        {
            LogRecords = new LogRecordsCollection(new ObservableCollection<LogRecord>(GetLogFIles(SelectedItem).SelectMany(file => file.LogRecords))); 
            LogRecords.GroupDescriptions.Add(new PropertyGroupDescription("FileName"));
        }

        private IEnumerable<LogFile> GetLogFIles(NavigationNode node)
        {
            var stack = new Stack<NavigationNode>();
            stack.Push(node);
            while (stack.Count != 0)
            {
                var currentNode = stack.Pop();
                var product = currentNode as Product;
                if (product == null)
                {
                    foreach (var child in currentNode.Children)
                    {
                        stack.Push(child);
                    }                    
                }
                else
                {
                    foreach (var logFile in product.LogFiles)
                    {
                        yield return logFile;
                    }
                }
            }
        }
    }
}