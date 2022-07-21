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

using Google.Apis.Logging;
using Google.Solutions.WWAuth.Adapters;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Solutions.WWAuth.Test.Adapter
{
    [TestFixture]
    public class TestServiceAccountAdapter
    {
        //---------------------------------------------------------------------
        // IsEnabled.
        //---------------------------------------------------------------------

        [Test]
        public void WhenServiceAccountEmpty_ThenIsEnabledReturnsFalse(
            [Values("", null)] string missingValue)
        {
            var adapter = new ServiceAccountAdapter(
                missingValue,
                new NullLogger());

            Assert.IsFalse(adapter.IsEnabled);
        }

        [Test]
        public void WhenServiceAccountSet_ThenIsEnabledReturnsTrue()
        {
            var adapter = new ServiceAccountAdapter(
                "securetoken@system.gserviceaccount.com",
                new NullLogger());

            Assert.IsTrue(adapter.IsEnabled);
        }

        //---------------------------------------------------------------------
        // Exists.
        //---------------------------------------------------------------------

        [Test]
        public async Task WhenServiceAccountIsWellKnown_ThenExistsReturnsTrue()
        {
            var adapter = new ServiceAccountAdapter(
                "securetoken@system.gserviceaccount.com",
                new NullLogger());

            Assert.IsTrue(await adapter.ExistsAsync(
                    CancellationToken.None)
                .ConfigureAwait(false));
        }

        [Test]
        public async Task WhenServiceAccountIsInvalid_ThenExistsReturnsFalse()
        {
            var adapter = new ServiceAccountAdapter(
                "does-not-exist@invalid.iam.gserviceaccount.com",
                new NullLogger());

            Assert.IsFalse(await adapter.ExistsAsync(
                    CancellationToken.None)
                .ConfigureAwait(false));
        }

        //---------------------------------------------------------------------
        // IntrospectToken.
        //---------------------------------------------------------------------

        [Test]
        public void WhenTokenInvalid_ThenIntrospectTokenThrowsException()
        {
            var adapter = new ServiceAccountAdapter(
                "does-not-exist@invalid.iam.gserviceaccount.com",
                new NullLogger());

            ExceptionAssert.ThrowsAggregateException<TokenExchangeException>(
                () => adapter.IntrospectTokenAsync(
                    "notatoken",
                    CancellationToken.None).Wait());
        }
    }
}
