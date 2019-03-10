// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace System.Collections.Immutable.Covariance
{
    public static class ImmutableCovariantList
    {
        /// <summary>
        /// Gets an empty list that retains the same sort semantics that this instance has.
        /// </summary>
        [Pure]
        public static IImmutableCovariantList<T> Clear<T>(this IImmutableCovariantList<T> list)
            => (list as ImmutableList<T>)?.Clear() ?? ImmutableList<T>.Empty;

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the
        /// first occurrence within the range of elements in the <see cref="ImmutableList{T}"/>
        /// that starts at the specified index and contains the specified number of elements.
        /// </summary>        
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="item">
        /// The object to locate in the <see cref="ImmutableList{T}"/>. The value
        /// can be null for reference types.
        /// </param>
        /// <param name="index">
        /// The zero-based starting index of the search. 0 (zero) is valid in an empty
        /// list.
        /// </param>
        /// <param name="count">
        /// The number of elements in the section to search.
        /// </param>
        /// <param name="equalityComparer">
        /// The equality comparer to use in the search.
        /// If <c>null</c>, <see cref="EqualityComparer{T}.Default"/> is used.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of item within the range of
        /// elements in the <see cref="ImmutableList{T}"/> that starts at index and
        /// contains count number of elements, if found; otherwise, -1.
        /// </returns>
        [Pure]
        public static int IndexOf<T>(this IImmutableCovariantList<T> list, T item, int index, int count, IEqualityComparer<T> equalityComparer)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).IndexOf(item, index, count, equalityComparer);

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the
        /// last occurrence within the range of elements in the <see cref="ImmutableList{T}"/>
        /// that contains the specified number of elements and ends at the specified
        /// index.
        /// </summary>           
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="item">
        /// The object to locate in the <see cref="ImmutableList{T}"/>. The value
        /// can be null for reference types.
        /// </param>
        /// <param name="index">The starting position of the search. The search proceeds from <paramref name="index"/> toward the beginning of this instance.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="equalityComparer">
        /// The equality comparer to use in the search.
        /// If <c>null</c>, <see cref="EqualityComparer{T}.Default"/> is used.
        /// </param>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="item"/> within the range of elements
        /// in the <see cref="ImmutableList{T}"/> that contains <paramref name="count"/> number of elements
        /// and ends at <paramref name="index"/>, if found; otherwise, -1.
        /// </returns>
        [Pure]
        public static int LastIndexOf<T>(this IImmutableCovariantList<T> list, T item, int index, int count, IEqualityComparer<T> equalityComparer)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).LastIndexOf(item, index, count, equalityComparer);

        /// <summary>
        /// Adds the specified value to this list.
        /// </summary>         
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>A new list with the element added.</returns>
        [Pure]
        public static IImmutableCovariantList<T> Add<T>(this IImmutableCovariantList<T> list, T value)
            => (list as ImmutableList<T>)?.Add(value) ?? ImmutableList.CreateRange(list.Append(value));

        /// <summary>
        /// Adds the specified values to this list.
        /// </summary>        
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="items">The values to add.</param>
        /// <returns>A new list with the elements added.</returns>
        [Pure]
        public static IImmutableCovariantList<T> AddRange<T>(this IImmutableCovariantList<T> list, IEnumerable<T> items)
            => (list as ImmutableList<T>)?.AddRange(items) ?? ImmutableList.CreateRange(list.Concat(items));

        /// <summary>
        /// Inserts the specified value at the specified index.
        /// </summary>         
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="index">The index at which to insert the value.</param>
        /// <param name="element">The element to insert.</param>
        /// <returns>The new immutable list.</returns>
        [Pure]
        public static IImmutableCovariantList<T> Insert<T>(this IImmutableCovariantList<T> list, int index, T element)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).Insert(index, element);

        /// <summary>
        /// Inserts the specified values at the specified index.
        /// </summary>         
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="index">The index at which to insert the value.</param>
        /// <param name="items">The elements to insert.</param>
        /// <returns>The new immutable list.</returns>
        [Pure]
        public static IImmutableCovariantList<T> InsertRange<T>(this IImmutableCovariantList<T> list, int index, IEnumerable<T> items)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).InsertRange(index, items);

        /// <summary>
        /// Removes the specified value from this list.
        /// </summary>        
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="value">The value to remove.</param>
        /// <param name="equalityComparer">
        /// The equality comparer to use in the search.
        /// If <c>null</c>, <see cref="EqualityComparer{T}.Default"/> is used.
        /// </param>
        /// <returns>A new list with the element removed, or this list if the element is not in this list.</returns>
        [Pure]
        public static IImmutableCovariantList<T> Remove<T>(this IImmutableCovariantList<T> list, T value, IEqualityComparer<T> equalityComparer)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).Remove(value, equalityComparer);

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified
        /// predicate.
        /// </summary>        
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="match">
        /// The <see cref="Predicate{T}"/> delegate that defines the conditions of the elements
        /// to remove.
        /// </param>
        /// <returns>
        /// The new list.
        /// </returns>
        [Pure]
        public static IImmutableCovariantList<T> RemoveAll<T>(this IImmutableCovariantList<T> list, Predicate<T> match)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).RemoveAll(match);

        /// <summary>
        /// Removes the specified values from this list.
        /// </summary>        
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="items">The items to remove if matches are found in this list.</param>
        /// <param name="equalityComparer">
        /// The equality comparer to use in the search.
        /// If <c>null</c>, <see cref="EqualityComparer{T}.Default"/> is used.
        /// </param>
        /// <returns>
        /// A new list with the elements removed.
        /// </returns>
        [Pure]
        public static IImmutableCovariantList<T> RemoveRange<T>(this IImmutableCovariantList<T> list, IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).RemoveRange(items, equalityComparer);

        /// <summary>
        /// Removes the specified values from this list.
        /// </summary>        
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="index">The starting index to begin removal.</param>
        /// <param name="count">The number of elements to remove.</param>
        /// <returns>
        /// A new list with the elements removed.
        /// </returns>
        [Pure]
        public static IImmutableCovariantList<T> RemoveRange<T>(this IImmutableCovariantList<T> list, int index, int count)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).RemoveRange(index, count);

        /// <summary>
        /// Removes the element at the specified index.
        /// </summary>        
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="index">The index.</param>
        /// <returns>A new list with the elements removed.</returns>
        [Pure]
        public static IImmutableCovariantList<T> RemoveAt<T>(this IImmutableCovariantList<T> list, int index)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).RemoveAt(index);

        /// <summary>
        /// Replaces an element in the list at a given position with the specified element.
        /// </summary>        
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="index">The position in the list of the element to replace.</param>
        /// <param name="value">The element to replace the old element with.</param>
        /// <returns>The new list -- even if the value being replaced is equal to the new value for that position.</returns>
        [Pure]
        public static IImmutableCovariantList<T> SetItem<T>(this IImmutableCovariantList<T> list, int index, T value)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).SetItem(index, value);

        /// <summary>
        /// Replaces the first equal element in the list with the specified element.
        /// </summary>        
        /// <param name="list">The immutable list to operate on.</param>
        /// <param name="oldValue">The element to replace.</param>
        /// <param name="newValue">The element to replace the old element with.</param>
        /// <param name="equalityComparer">
        /// The equality comparer to use in the search.
        /// If <c>null</c>, <see cref="EqualityComparer{T}.Default"/> is used.
        /// </param>
        /// <returns>The new list -- even if the value being replaced is equal to the new value for that position.</returns>
        /// <exception cref="ArgumentException">Thrown when the old value does not exist in the list.</exception>
        [Pure]
        public static IImmutableCovariantList<T> Replace<T>(this IImmutableCovariantList<T> list, T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
            => (list as ImmutableList<T> ?? list.ToImmutableList()).Replace(oldValue, newValue, equalityComparer);
    }
}
