﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Bumblebee.Interfaces;

using OpenQA.Selenium;

namespace Bumblebee.Implementation
{
	public class TableRow : Block, ITableRow
	{
		private readonly IDictionary<string, string> _data;

		public string this[int index] => _data.Values.ElementAt(index);

		public string this[string column] => _data[column];

		public TableRow(IBlock parent, By @by) : base(parent, @by)
		{
			_data = Parent
				.FindElement(By.TagName("thead"))
				.FindElement(By.TagName("tr"))
				.FindElements(By.TagName("th"))
				.Zip(FindElements(By.TagName("td")), (header, cell) => new KeyValuePair<string, string>(header.Text, cell.Text))
				.ToDictionary(x => x.Key, x => x.Value);
		}


		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<string> GetEnumerator()
		{
			return _data.Values.GetEnumerator();
		}
	}
}
