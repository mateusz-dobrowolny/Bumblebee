﻿using System;

using Bumblebee.Extensions;
using Bumblebee.Interfaces;

using OpenQA.Selenium;

namespace Bumblebee.Implementation
{
	public class DateField : TextField, IDateField
	{
		public DateField(IBlock parent, By @by) : base(parent, @by)
		{
		}

		public virtual TResult EnterDate<TResult>(DateTime date) where TResult : IBlock
		{
			var executor = (IJavaScriptExecutor) Session.Driver;

			executor.ExecuteScript($"arguments[0].value = '{date:yyyy-MM-dd}';", Tag);

			executor.ExecuteScript($"arguments[0].value = '{date:yyyy-MM-dd}';", Tag);

			return this.FindRelated<TResult>();
		}

		public virtual DateTime? Value
		{
			get
			{
				DateTime? result = null;

				DateTime date;
				if ((Text != null) && DateTime.TryParse(Text, out date))
				{
					result = date;
				}

				return result;
			}
		}
	}

	public class DateField<TResult> : DateField, IDateField<TResult>
		where TResult : IBlock
	{
		public DateField(IBlock parent, By @by) : base(parent, @by)
		{
		}

		public virtual TResult EnterDate(DateTime date)
		{
			return EnterDate<TResult>(date);
		}

		public TResult Press(Key key)
		{
			return Press<TResult>(key);
		}

		public virtual TResult EnterText(string text)
		{
			return EnterText<TResult>(text);
		}

		public virtual TResult AppendText(string text)
		{
			return AppendText<TResult>(text);
		}
	}
}
