using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

using OpenQA.Selenium;

namespace Bumblebee.Specifications
{
	internal class ByFunctionWithSingleOutput : By
	{
		private readonly Func<ISearchContext, IWebElement> _function;

		public ByFunctionWithSingleOutput(Expression<Func<ISearchContext, IWebElement>> expression)
		{
			_function = expression.Compile();
			Description = $"By.Function: {expression.Body}";
		}

		public override IWebElement FindElement(ISearchContext context)
		{
			var element = _function(context);

			if (element == null)
			{
				throw new NoSuchElementException();
			}

			return element;
		}

		public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
		{
			var result = new List<IWebElement>(1);

			var element = _function(context);

			if (element != null)
			{
				result.Add(element);
			}

			return new ReadOnlyCollection<IWebElement>(result);
		}
	}
}
