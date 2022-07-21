﻿//
// Copyright 2022 Google LLC
//
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//

using System.ComponentModel;
using System.Windows.Forms;

namespace Google.Solutions.WWAuth.View
{
    public interface IPropertiesSheet
    {
        /// <summary>
        /// View model backing this property sheet
        /// page.
        /// </summary>
        IPropertiesSheetViewModel ViewModel { get; }

        /// <summary>
        /// Invoked when the sheet has become the currently
        /// active sheet.
        /// </summary>
        void OnActivated();
    }

    public interface IPropertiesSheetViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Title of property sheet.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// True if any values on property sheet have been
        /// changed.
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// Apply and persist changes.
        /// </summary>
        DialogResult ApplyChanges(IWin32Window owner);

        /// <summary>
        /// Validate current settings, throws an exception
        /// on failure.
        /// </summary>
        void ValidateChanges();
    }
}
