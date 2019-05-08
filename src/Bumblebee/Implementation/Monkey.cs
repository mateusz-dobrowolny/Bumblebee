﻿using System;
using System.Collections.Generic;

using Bumblebee.Interfaces;

namespace Bumblebee.Implementation
{
	public abstract class Monkey : IMonkey
	{
		private double _probability;

		protected double Probability
		{
			get { return _probability; }
			set { SetProbability(value); }
		}

		public virtual void SetProbability(double probability)
		{
			if ((probability > 1) || (probability < 0))
			{
				throw new FormatException("Probability must be between 0 and 1");
			}

			_probability = probability;
		}

		protected IBlock Block { get; set; }

		public IList<string> Logs { get; protected set; }

		public abstract void PerformRandomAction();

		public abstract void Invoke(IBlock block);

		public abstract void VerifyState();
	}
}
