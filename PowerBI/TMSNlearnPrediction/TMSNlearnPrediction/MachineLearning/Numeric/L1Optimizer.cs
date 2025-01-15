using System;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000444 RID: 1092
	public sealed class L1Optimizer : Optimizer
	{
		// Token: 0x060016A4 RID: 5796 RVA: 0x00084B98 File Offset: 0x00082D98
		public L1Optimizer(IHostEnvironment env, int biasCount, float l1weight, int m = 20, bool keepDense = false, ITerminationCriterion term = null)
			: base(env, m, keepDense, term)
		{
			Contracts.Check(this._env, biasCount >= 0);
			Contracts.Check(this._env, l1weight >= 0f);
			this._biasCount = biasCount;
			this._l1weight = l1weight;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00084BE8 File Offset: 0x00082DE8
		internal override Optimizer.OptimizerState MakeState(IChannel ch, IProgressChannelProvider progress, DifferentiableFunction function, ref VBuffer<float> initial)
		{
			if (this._l1weight > 0f && this._biasCount < initial.Length)
			{
				return new L1Optimizer.L1OptimizerState(ch, progress, function, ref initial, base.M, base.TotalMemoryLimit, this._biasCount, this._l1weight, this._keepDense);
			}
			return new Optimizer.FunctionOptimizerState(ch, progress, function, ref initial, base.M, base.TotalMemoryLimit, this._keepDense);
		}

		// Token: 0x04000DEA RID: 3562
		private readonly int _biasCount;

		// Token: 0x04000DEB RID: 3563
		private readonly float _l1weight;

		// Token: 0x02000445 RID: 1093
		public sealed class L1OptimizerState : Optimizer.OptimizerState
		{
			// Token: 0x060016A6 RID: 5798 RVA: 0x00084C56 File Offset: 0x00082E56
			internal L1OptimizerState(IChannel ch, IProgressChannelProvider progress, DifferentiableFunction function, ref VBuffer<float> initial, int m, long totalMemLimit, int biasCount, float l1Weight, bool keepDense)
				: base(ch, progress, ref initial, m, totalMemLimit, keepDense)
			{
				this._biasCount = biasCount;
				this._l1weight = l1Weight;
				this._function = function;
				this._steepestDescDir = base.CreateWorkingVector();
				this.Init();
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00084C91 File Offset: 0x00082E91
			public override DifferentiableFunction Function
			{
				get
				{
					return new DifferentiableFunction(this.EvalCore);
				}
			}

			// Token: 0x060016A8 RID: 5800 RVA: 0x00084CE0 File Offset: 0x00082EE0
			private float EvalCore(ref VBuffer<float> input, ref VBuffer<float> gradient, IProgressChannelProvider progress)
			{
				float res = 0f;
				if (this._biasCount > 0)
				{
					VBufferUtils.ForEachDefined<float>(ref input, delegate(int ind, float value)
					{
						if (ind >= this._biasCount)
						{
							res += Math.Abs(value);
						}
					});
				}
				else
				{
					VBufferUtils.ForEachDefined<float>(ref input, delegate(int ind, float value)
					{
						res += Math.Abs(value);
					});
				}
				res = this._l1weight * res + this._function(ref input, ref gradient, progress);
				return res;
			}

			// Token: 0x060016A9 RID: 5801 RVA: 0x00084D5C File Offset: 0x00082F5C
			public override float Eval(ref VBuffer<float> input, ref VBuffer<float> gradient)
			{
				return this.EvalCore(ref input, ref gradient, this._progressProvider);
			}

			// Token: 0x060016AA RID: 5802 RVA: 0x00084DD5 File Offset: 0x00082FD5
			private void MakeSteepestDescDir()
			{
				VBufferUtils.ApplyInto<float, float, float>(ref this._x, ref this._grad, ref this._steepestDescDir, delegate(int ind, float xVal, float gradVal)
				{
					if (ind < this._biasCount)
					{
						return -gradVal;
					}
					if (xVal < 0f)
					{
						return -gradVal + this._l1weight;
					}
					if (xVal > 0f)
					{
						return -gradVal - this._l1weight;
					}
					if (gradVal < -this._l1weight)
					{
						return -gradVal - this._l1weight;
					}
					if (gradVal > this._l1weight)
					{
						return -gradVal + this._l1weight;
					}
					return 0f;
				});
				this._steepestDescDir.CopyTo(ref this._dir);
			}

			// Token: 0x060016AB RID: 5803 RVA: 0x00084E1C File Offset: 0x0008301C
			private void FixDirZeros()
			{
				VBufferUtils.ApplyWithEitherDefined<float, float>(ref this._steepestDescDir, ref this._dir, delegate(int i, float sdVal, ref float dirVal)
				{
					if (sdVal == 0f)
					{
						dirVal = 0f;
					}
				});
			}

			// Token: 0x060016AC RID: 5804 RVA: 0x00084E6E File Offset: 0x0008306E
			private void GetNextPoint(float alpha)
			{
				VectorUtils.AddMultInto(ref this._x, alpha, ref this._dir, ref this._newX);
				VBufferUtils.ApplyWith<float, float>(ref this._x, ref this._newX, delegate(int ind, float xVal, ref float newXval)
				{
					if ((double)(xVal * newXval) < 0.0 && ind >= this._biasCount)
					{
						newXval = 0f;
					}
				});
			}

			// Token: 0x060016AD RID: 5805 RVA: 0x00084EA5 File Offset: 0x000830A5
			internal override void UpdateDir()
			{
				this.MakeSteepestDescDir();
				base.MapDirByInverseHessian();
				this.FixDirZeros();
			}

			// Token: 0x060016AE RID: 5806 RVA: 0x00084ED8 File Offset: 0x000830D8
			internal override bool LineSearch(IChannel ch, bool force)
			{
				float num = -VectorUtils.DotProduct(ref this._dir, ref this._steepestDescDir);
				if (num == 0f)
				{
					throw ch.Process(new Optimizer.PrematureConvergenceException(this, "Directional derivative is zero. You may be sitting on the optimum."));
				}
				Contracts.Check(ch, num < 0f, "L-BFGS chose a non-descent direction.");
				float num2 = ((this._iter == 1) ? (1f / VectorUtils.Norm(ref this._dir)) : 1f);
				this.GetNextPoint(num2);
				float num3 = VectorUtils.DotProduct(ref this._steepestDescDir, ref this._newX) - VectorUtils.DotProduct(ref this._steepestDescDir, ref this._x);
				if (num3 < 0f)
				{
					VBufferUtils.ApplyWith<float, float>(ref this._steepestDescDir, ref this._dir, delegate(int ind, float sdVal, ref float dirVal)
					{
						if (sdVal * dirVal < 0f && ind >= this._biasCount)
						{
							dirVal = 0f;
						}
					});
					this.GetNextPoint(num2);
					num3 = VectorUtils.DotProduct(ref this._steepestDescDir, ref this._newX) - VectorUtils.DotProduct(ref this._steepestDescDir, ref this._x);
				}
				int num4 = 0;
				for (;;)
				{
					this._newValue = this.Eval(ref this._newX, ref this._newGrad);
					this._gradientCalculations++;
					if (this._newValue <= this._value - 0.0001f * num3)
					{
						break;
					}
					num4++;
					if (!force && num4 == 8)
					{
						return false;
					}
					num2 *= 0.25f;
					this.GetNextPoint(num2);
					num3 = VectorUtils.DotProduct(ref this._steepestDescDir, ref this._newX) - VectorUtils.DotProduct(ref this._steepestDescDir, ref this._x);
				}
				return true;
			}

			// Token: 0x04000DEC RID: 3564
			private const float _GAMMA = 0.0001f;

			// Token: 0x04000DED RID: 3565
			private const int _MAX_LINE_SEARCH = 8;

			// Token: 0x04000DEE RID: 3566
			private readonly DifferentiableFunction _function;

			// Token: 0x04000DEF RID: 3567
			private readonly int _biasCount;

			// Token: 0x04000DF0 RID: 3568
			private readonly float _l1weight;

			// Token: 0x04000DF1 RID: 3569
			private VBuffer<float> _steepestDescDir;
		}
	}
}
