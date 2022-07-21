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

using Google.Apis.Util;
using Google.Solutions.WWAuth.Data;
using System.Linq;
using System.Windows.Forms;

namespace Google.Solutions.WWAuth.View
{
    internal class ViewTokenViewModel : ViewModelBase, IPropertiesSheetViewModel
    {
        private readonly ISubjectToken token;

        public string Title => "Attributes";

        public bool IsDirty => false;

        public ViewTokenViewModel(ISubjectToken token)
        {
            this.token = token.ThrowIfNull(nameof(token));
        }

        public string Issuer => this.token.Issuer;

        public string Audience => this.token.IsEncrypted
            ? "(encrypted)"
            : this.token.Audience;

        public string Expiry => this.token.IsEncrypted
            ? "(encrypted)"
            : this.token.Expiry?.ToString();

        public ListViewItem[] Attributes
            => this.token.Attributes
                .Select(kvp => new ListViewItem(new[]
                {
                    kvp.Key,
                    kvp.Value.ToString()
                }))
                .ToArray();

        //---------------------------------------------------------------------
        // Actions.
        //---------------------------------------------------------------------

        public DialogResult ApplyChanges(IWin32Window owner)
        {
            return DialogResult.OK;
        }

        public void ValidateChanges()
        {
        }
    }
}
