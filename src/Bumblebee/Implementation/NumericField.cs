﻿using System;
using System.Globalization;

using Bumblebee.Extensions;
using Bumblebee.Interfaces;

using OpenQA.Selenium;

namespace Bumblebee.Implementation
{
	public class NumericField : TextField, INumericField
	{
		public NumericField(IBlock parent, By @by) : base(parent, @by)
		{
		}

		public virtual TResult EnterNumber<TResult>(double number) where TResult : IBlock
		{
			Tag.Clear();

			Tag.SendKeys(number.ToString(CultureInfo.CurrentUICulture));

			return this.FindRelated<TResult>();
		}

		public virtual double? Value
		{
			get
			{
				double? result = null;

				double x;
				if ((Text != null) && Double.TryParse(Text, out x))
				{
					result = x;
				}

				return result;
			}
		}
	}

	public class NumericField<TResult> : NumericField, INumericField<TResult>
		where TResult : IBlock
	{
		public NumericField(IBlock parent, By @by) : base(parent, @by)
		{
		}

		public virtual TResult EnterNumber(double number)
		{
			return EnterNumber<TResult>(number);
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
