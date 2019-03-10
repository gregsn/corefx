// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace System.Collections.Immutable
{
    /// <summary>
    /// A covariant view onto an immutable list. Extension methods can be found here: <see cref="System.Collections.Immutable.Covariance"/> 
    /// Example: adding an apple to a list of bananas gives you a fresh list of fruits leaving the banana list untouched.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public interface IImmutableCovariantList<out T> : IReadOnlyList<T>
    {
    }
}
