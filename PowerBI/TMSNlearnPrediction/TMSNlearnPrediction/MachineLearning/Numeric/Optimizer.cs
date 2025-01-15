using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200043E RID: 1086
	public class Optimizer
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00083DA5 File Offset: 0x00081FA5
		public int M
		{
			get
			{
				return this._m;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00083DAD File Offset: 0x00081FAD
		public long TotalMemoryLimit
		{
			get
			{
				return this._totalMemLimit;
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00083DB8 File Offset: 0x00081FB8
		public Optimizer(IHostEnvironment env, int m = 20, bool keepDense = false, ITerminationCriterion term = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._env = env;
			this._m = m;
			this._keepDense = keepDense;
			this._staticTerm = term ?? new MeanRelativeImprovementCriterion(0.0001f, 5, int.MaxValue);
			this._totalMemLimit = -1L;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00083E0F File Offset: 0x0008200F
		internal virtual Optimizer.OptimizerState MakeState(IChannel ch, IProgressChannelProvider progress, DifferentiableFunction function, ref VBuffer<float> initial)
		{
			return new Optimizer.FunctionOptimizerState(ch, progress, function, ref initial, this._m, this._totalMemLimit, this._keepDense);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00083E30 File Offset: 0x00082030
		public void Minimize(DifferentiableFunction function, ref VBuffer<float> initial, float tolerance, ref VBuffer<float> result, out float optimum)
		{
			ITerminationCriterion terminationCriterion = new MeanRelativeImprovementCriterion(tolerance, 5, int.MaxValue);
			this.Minimize(function, ref initial, terminationCriterion, ref result, out optimum);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00083E57 File Offset: 0x00082057
		public void Minimize(DifferentiableFunction function, ref VBuffer<float> initial, ref VBuffer<float> result, out float optimum)
		{
			this.Minimize(function, ref initial, this._staticTerm, ref result, out optimum);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00083E9C File Offset: 0x0008209C
		public void Minimize(DifferentiableFunction function, ref VBuffer<float> initial, ITerminationCriterion term, ref VBuffer<float> result, out float optimum)
		{
			using (IProgressChannel progressChannel = this._env.StartProgressChannel("LBFGS Optimizer"))
			{
				using (IChannel channel = this._env.Start("LBFGS Optimizer"))
				{
					channel.Info("Beginning optimization");
					channel.Info("num vars: {0}", new object[] { initial.Length });
					channel.Info("improvement criterion: {0}", new object[] { term.FriendlyName });
					Optimizer.OptimizerState state = this.MakeState(channel, progressChannel, function, ref initial);
					term.Reset();
					ProgressHeader progressHeader = new ProgressHeader(new string[] { "Loss", "Improvement" }, new string[] { "iterations", "gradients" });
					progressChannel.SetHeader(progressHeader, delegate(IProgressEntry e)
					{
						e.SetProgress(0, (double)(state.Iter - 1));
						e.SetProgress(1, (double)state.GradientCalculations);
					});
					bool flag = false;
					progressChannel.Checkpoint(new double?[]
					{
						new double?((double)state.Value),
						null,
						new double?(0.0)
					});
					state.UpdateDir();
					while (!flag)
					{
						if (!state.LineSearch(channel, false))
						{
							state.DiscardOldVectors();
							state.UpdateDir();
							state.LineSearch(channel, true);
						}
						string text;
						flag = term.Terminate(state, out text);
						double? num = null;
						double num2;
						int num3;
						if (text != null && DoubleParser.TryParse(ref num2, text, 0, text.Length, ref num3))
						{
							num = new double?(num2);
						}
						progressChannel.Checkpoint(new double?[]
						{
							new double?((double)state.Value),
							num,
							new double?((double)state.Iter)
						});
						if (!flag)
						{
							state.Shift();
							state.UpdateDir();
						}
					}
					state.X.CopyTo(ref result);
					optimum = state.Value;
					channel.Done();
				}
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00084168 File Offset: 0x00082368
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x00084170 File Offset: 0x00082370
		public bool Quiet
		{
			get
			{
				return this.quiet;
			}
			set
			{
				this.quiet = value;
			}
		}

		// Token: 0x04000DCD RID: 3533
		private int _m;

		// Token: 0x04000DCE RID: 3534
		private long _totalMemLimit;

		// Token: 0x04000DCF RID: 3535
		private ITerminationCriterion _staticTerm;

		// Token: 0x04000DD0 RID: 3536
		protected readonly bool _keepDense;

		// Token: 0x04000DD1 RID: 3537
		protected readonly IHostEnvironment _env;

		// Token: 0x04000DD2 RID: 3538
		private bool quiet;

		// Token: 0x0200043F RID: 1087
		public class OptimizerException : Exception
		{
			// Token: 0x17000207 RID: 519
			// (get) Token: 0x0600168B RID: 5771 RVA: 0x00084179 File Offset: 0x00082379
			public Optimizer.OptimizerState State
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x0600168C RID: 5772 RVA: 0x00084181 File Offset: 0x00082381
			internal OptimizerException(Optimizer.OptimizerState state, string message)
				: base(message)
			{
				this._state = state;
			}

			// Token: 0x04000DD3 RID: 3539
			private readonly Optimizer.OptimizerState _state;
		}

		// Token: 0x02000440 RID: 1088
		public abstract class OptimizerState
		{
			// Token: 0x17000208 RID: 520
			// (get) Token: 0x0600168D RID: 5773
			public abstract DifferentiableFunction Function { get; }

			// Token: 0x0600168E RID: 5774
			public abstract float Eval(ref VBuffer<float> input, ref VBuffer<float> gradient);

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x0600168F RID: 5775 RVA: 0x00084191 File Offset: 0x00082391
			public VBuffer<float> X
			{
				get
				{
					return this._newX;
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06001690 RID: 5776 RVA: 0x00084199 File Offset: 0x00082399
			public VBuffer<float> Grad
			{
				get
				{
					return this._newGrad;
				}
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06001691 RID: 5777 RVA: 0x000841A1 File Offset: 0x000823A1
			public VBuffer<float> LastDir
			{
				get
				{
					return this._dir;
				}
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06001692 RID: 5778 RVA: 0x000841A9 File Offset: 0x000823A9
			public float Value
			{
				get
				{
					return this._newValue;
				}
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x06001693 RID: 5779 RVA: 0x000841B1 File Offset: 0x000823B1
			public float LastValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06001694 RID: 5780 RVA: 0x000841B9 File Offset: 0x000823B9
			public int Iter
			{
				get
				{
					return this._iter;
				}
			}

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x06001695 RID: 5781 RVA: 0x000841C1 File Offset: 0x000823C1
			public int GradientCalculations
			{
				get
				{
					return this._gradientCalculations;
				}
			}

			// Token: 0x06001696 RID: 5782 RVA: 0x000841CC File Offset: 0x000823CC
			protected OptimizerState(IChannel ch, IProgressChannelProvider progress, ref VBuffer<float> initial, int m, long totalMemLimit, bool keepDense)
			{
				this._ch = ch;
				this._progressProvider = progress;
				this._iter = 1;
				this._keepDense = keepDense;
				this.Dim = initial.Length;
				this._x = this.CreateWorkingVector();
				initial.CopyTo(ref this._x);
				this._m = m;
				this._totalMemLimit = totalMemLimit;
				this.Dim = initial.Length;
				this._grad = this.CreateWorkingVector();
				this._dir = this.CreateWorkingVector();
				this._newX = this.CreateWorkingVector();
				this._newGrad = this.CreateWorkingVector();
				this._sList = new VBuffer<float>[this._m];
				this._yList = new VBuffer<float>[this._m];
				this._roList = new List<float>();
			}

			// Token: 0x06001697 RID: 5783 RVA: 0x00084299 File Offset: 0x00082499
			protected VBuffer<float> CreateWorkingVector()
			{
				if (!this._keepDense)
				{
					return VBufferUtils.CreateEmpty<float>(this.Dim);
				}
				return VBufferUtils.CreateDense<float>(this.Dim);
			}

			// Token: 0x06001698 RID: 5784 RVA: 0x000842BC File Offset: 0x000824BC
			protected virtual void Init()
			{
				this._newValue = (this._value = this.Eval(ref this._x, ref this._grad));
				this._gradientCalculations++;
				if (!FloatUtils.IsFinite(this._value))
				{
					throw Contracts.Except(this._ch, "Optimizer unable to proceed with loss function yielding {0}", new object[] { this._value });
				}
			}

			// Token: 0x06001699 RID: 5785 RVA: 0x0008432C File Offset: 0x0008252C
			internal void MapDirByInverseHessian()
			{
				int count = this._roList.Count;
				if (count != 0)
				{
					float[] array = new float[count];
					int num = -1;
					for (int i = count - 1; i >= 0; i--)
					{
						if (this._roList[i] > 0f)
						{
							array[i] = -VectorUtils.DotProduct(ref this._sList[i], ref this._dir) / this._roList[i];
							VectorUtils.AddMult(ref this._yList[i], array[i], ref this._dir);
							if (num == -1)
							{
								num = i;
							}
						}
					}
					if (num == -1)
					{
						return;
					}
					float num2 = VectorUtils.DotProduct(ref this._yList[num], ref this._yList[num]);
					VectorUtils.ScaleBy(ref this._dir, this._roList[num] / num2);
					for (int j = 0; j <= num; j++)
					{
						if (this._roList[j] > 0f)
						{
							float num3 = VectorUtils.DotProduct(ref this._yList[j], ref this._dir) / this._roList[j];
							VectorUtils.AddMult(ref this._sList[j], -array[j] - num3, ref this._dir);
						}
					}
				}
			}

			// Token: 0x0600169A RID: 5786 RVA: 0x00084467 File Offset: 0x00082667
			internal void DiscardOldVectors()
			{
				this._roList.Clear();
				Array.Clear(this._sList, 0, this._sList.Length);
				Array.Clear(this._yList, 0, this._yList.Length);
			}

			// Token: 0x0600169B RID: 5787 RVA: 0x0008449C File Offset: 0x0008269C
			internal virtual void UpdateDir()
			{
				VectorUtils.ScaleInto(ref this._grad, -1f, ref this._dir);
				this.MapDirByInverseHessian();
			}

			// Token: 0x0600169C RID: 5788 RVA: 0x000844BC File Offset: 0x000826BC
			internal void Shift()
			{
				if (this._roList.Count < this._m && this._totalMemLimit > 0L)
				{
					long totalMemory = GC.GetTotalMemory(true);
					if (totalMemory > this._totalMemLimit)
					{
						this._m = this._roList.Count;
					}
				}
				VBuffer<float> vbuffer;
				VBuffer<float> vbuffer2;
				if (this._roList.Count == this._m)
				{
					vbuffer = this._sList[0];
					Array.Copy(this._sList, 1, this._sList, 0, this._m - 1);
					vbuffer2 = this._yList[0];
					Array.Copy(this._yList, 1, this._yList, 0, this._m - 1);
					this._roList.RemoveAt(0);
				}
				else
				{
					vbuffer = this.CreateWorkingVector();
					vbuffer2 = this.CreateWorkingVector();
				}
				VectorUtils.AddMultInto(ref this._newX, -1f, ref this._x, ref vbuffer);
				VectorUtils.AddMultInto(ref this._newGrad, -1f, ref this._grad, ref vbuffer2);
				float num = VectorUtils.DotProduct(ref vbuffer, ref vbuffer2);
				if (num == 0f)
				{
					throw this._ch.Process(new Optimizer.PrematureConvergenceException(this, "ro equals zero. Is your function linear?"));
				}
				this._sList[this._roList.Count] = vbuffer;
				this._yList[this._roList.Count] = vbuffer2;
				this._roList.Add(num);
				Utils.Swap<float>(ref this._value, ref this._newValue);
				Utils.Swap<VBuffer<float>>(ref this._x, ref this._newX);
				Utils.Swap<VBuffer<float>>(ref this._grad, ref this._newGrad);
				this._iter++;
				this._gradientCalculations = 0;
			}

			// Token: 0x0600169D RID: 5789 RVA: 0x00084678 File Offset: 0x00082878
			internal virtual bool LineSearch(IChannel ch, bool force)
			{
				float num = VectorUtils.DotProduct(ref this._dir, ref this._grad);
				Contracts.Check(ch, num < 0f, "L-BFGS chose a non-descent direction.");
				float num2 = 0.0001f * num;
				float num3 = 0.9f * num;
				float num4 = ((this._iter == 1) ? (1f / VectorUtils.Norm(ref this._dir)) : 1f);
				Optimizer.OptimizerState.PointValueDeriv pointValueDeriv = new Optimizer.OptimizerState.PointValueDeriv(0f, this._value, num);
				Optimizer.OptimizerState.PointValueDeriv pointValueDeriv2 = default(Optimizer.OptimizerState.PointValueDeriv);
				Optimizer.OptimizerState.PointValueDeriv pointValueDeriv3 = default(Optimizer.OptimizerState.PointValueDeriv);
				Optimizer.OptimizerState.PointValueDeriv pointValueDeriv4;
				for (;;)
				{
					VectorUtils.AddMultInto(ref this._x, num4, ref this._dir, ref this._newX);
					this._newValue = this.Eval(ref this._newX, ref this._newGrad);
					this._gradientCalculations++;
					if (float.IsPositiveInfinity(this._newValue))
					{
						num4 /= 2f;
					}
					else
					{
						if (!FloatUtils.IsFinite(this._newValue))
						{
							break;
						}
						num = VectorUtils.DotProduct(ref this._dir, ref this._newGrad);
						pointValueDeriv4 = new Optimizer.OptimizerState.PointValueDeriv(num4, this._newValue, num);
						if (pointValueDeriv4.v > this._value + num2 * num4 || (pointValueDeriv.a > 0f && pointValueDeriv4.v >= pointValueDeriv.v))
						{
							goto IL_0153;
						}
						if (Math.Abs(pointValueDeriv4.d) <= -num3)
						{
							return true;
						}
						if (pointValueDeriv4.d >= 0f)
						{
							goto Block_7;
						}
						pointValueDeriv = pointValueDeriv4;
						if (num4 == 0f)
						{
							num4 = float.Epsilon;
						}
						else
						{
							num4 *= 2f;
						}
					}
				}
				throw Contracts.Except(ch, "Optimizer unable to proceed with loss function yielding {0}", new object[] { this._newValue });
				IL_0153:
				pointValueDeriv2 = pointValueDeriv;
				pointValueDeriv3 = pointValueDeriv4;
				goto IL_01AB;
				Block_7:
				pointValueDeriv2 = pointValueDeriv4;
				pointValueDeriv3 = pointValueDeriv;
				IL_01AB:
				float num5 = 0.01f;
				int num6 = 10;
				int num7 = 0;
				while (num7 != num6 || force)
				{
					Optimizer.OptimizerState.PointValueDeriv pointValueDeriv5 = ((pointValueDeriv2.a < pointValueDeriv3.a) ? pointValueDeriv2 : pointValueDeriv3);
					Optimizer.OptimizerState.PointValueDeriv pointValueDeriv6 = ((pointValueDeriv2.a < pointValueDeriv3.a) ? pointValueDeriv3 : pointValueDeriv2);
					if (pointValueDeriv5.d > 0f && pointValueDeriv6.d < 0f)
					{
						num4 = ((pointValueDeriv2.v < pointValueDeriv3.v) ? pointValueDeriv2.a : pointValueDeriv3.a);
					}
					else
					{
						num4 = Optimizer.OptimizerState.CubicInterp(pointValueDeriv2, pointValueDeriv3);
						if (float.IsNaN(num4) || float.IsInfinity(num4))
						{
							num4 = (pointValueDeriv2.a + pointValueDeriv3.a) / 2f;
						}
					}
					float num8 = num5 * pointValueDeriv5.a + (1f - num5) * pointValueDeriv6.a;
					if (num4 > num8)
					{
						num4 = num8;
					}
					float num9 = num5 * pointValueDeriv6.a + (1f - num5) * pointValueDeriv5.a;
					if (num4 < num9)
					{
						num4 = num9;
					}
					VectorUtils.AddMultInto(ref this._x, num4, ref this._dir, ref this._newX);
					this._newValue = this.Eval(ref this._newX, ref this._newGrad);
					this._gradientCalculations++;
					if (!FloatUtils.IsFinite(this._newValue))
					{
						throw Contracts.Except(ch, "Optimizer unable to proceed with loss function yielding {0}", new object[] { this._newValue });
					}
					num = VectorUtils.DotProduct(ref this._dir, ref this._newGrad);
					Optimizer.OptimizerState.PointValueDeriv pointValueDeriv7 = new Optimizer.OptimizerState.PointValueDeriv(num4, this._newValue, num);
					if (pointValueDeriv7.v > this._value + num2 * num4 || pointValueDeriv7.v >= pointValueDeriv2.v)
					{
						if (pointValueDeriv3.a == pointValueDeriv7.a)
						{
							if (force)
							{
								throw ch.Process(new Optimizer.PrematureConvergenceException(this, "Step size interval numerically zero."));
							}
							return false;
						}
						else
						{
							pointValueDeriv3 = pointValueDeriv7;
						}
					}
					else
					{
						if (Math.Abs(pointValueDeriv7.d) <= -num3)
						{
							return true;
						}
						if (pointValueDeriv7.d * (pointValueDeriv3.a - pointValueDeriv2.a) >= 0f)
						{
							pointValueDeriv3 = pointValueDeriv2;
						}
						if (pointValueDeriv2.a == pointValueDeriv7.a)
						{
							if (force)
							{
								throw ch.Process(new Optimizer.PrematureConvergenceException(this, "Step size interval numerically zero."));
							}
							return false;
						}
						else
						{
							pointValueDeriv2 = pointValueDeriv7;
						}
					}
					num7++;
				}
				return false;
			}

			// Token: 0x0600169E RID: 5790 RVA: 0x00084A7C File Offset: 0x00082C7C
			private static float CubicInterp(Optimizer.OptimizerState.PointValueDeriv p0, Optimizer.OptimizerState.PointValueDeriv p1)
			{
				double num = (double)(p0.d + p1.d - 3f * (p0.v - p1.v) / (p0.a - p1.a));
				double num2 = (double)Math.Sign(p1.a - p0.a) * Math.Sqrt(num * num - (double)(p0.d * p1.d));
				double num3 = (double)p1.d + num2 - num;
				double num4 = (double)(p1.d - p0.d) + 2.0 * num2;
				return (float)((double)p1.a - (double)(p1.a - p0.a) * num3 / num4);
			}

			// Token: 0x04000DD4 RID: 3540
			internal VBuffer<float> _x;

			// Token: 0x04000DD5 RID: 3541
			internal VBuffer<float> _grad;

			// Token: 0x04000DD6 RID: 3542
			internal VBuffer<float> _newX;

			// Token: 0x04000DD7 RID: 3543
			internal VBuffer<float> _newGrad;

			// Token: 0x04000DD8 RID: 3544
			internal VBuffer<float> _dir;

			// Token: 0x04000DD9 RID: 3545
			internal float _value;

			// Token: 0x04000DDA RID: 3546
			internal float _newValue;

			// Token: 0x04000DDB RID: 3547
			internal int _iter;

			// Token: 0x04000DDC RID: 3548
			protected int _gradientCalculations;

			// Token: 0x04000DDD RID: 3549
			public readonly int Dim;

			// Token: 0x04000DDE RID: 3550
			protected readonly IChannel _ch;

			// Token: 0x04000DDF RID: 3551
			protected readonly IProgressChannelProvider _progressProvider;

			// Token: 0x04000DE0 RID: 3552
			private readonly bool _keepDense;

			// Token: 0x04000DE1 RID: 3553
			private readonly VBuffer<float>[] _sList;

			// Token: 0x04000DE2 RID: 3554
			private readonly VBuffer<float>[] _yList;

			// Token: 0x04000DE3 RID: 3555
			private readonly List<float> _roList;

			// Token: 0x04000DE4 RID: 3556
			private int _m;

			// Token: 0x04000DE5 RID: 3557
			private readonly long _totalMemLimit;

			// Token: 0x02000441 RID: 1089
			private struct PointValueDeriv
			{
				// Token: 0x0600169F RID: 5791 RVA: 0x00084B38 File Offset: 0x00082D38
				public PointValueDeriv(float a, float value, float deriv)
				{
					this.a = a;
					this.v = value;
					this.d = deriv;
				}

				// Token: 0x04000DE6 RID: 3558
				public readonly float a;

				// Token: 0x04000DE7 RID: 3559
				public readonly float v;

				// Token: 0x04000DE8 RID: 3560
				public readonly float d;
			}
		}

		// Token: 0x02000442 RID: 1090
		public sealed class FunctionOptimizerState : Optimizer.OptimizerState
		{
			// Token: 0x060016A0 RID: 5792 RVA: 0x00084B4F File Offset: 0x00082D4F
			internal FunctionOptimizerState(IChannel ch, IProgressChannelProvider progress, DifferentiableFunction function, ref VBuffer<float> initial, int m, long totalMemLimit, bool keepDense)
				: base(ch, progress, ref initial, m, totalMemLimit, keepDense)
			{
				this._function = function;
				this.Init();
			}

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00084B6E File Offset: 0x00082D6E
			public override DifferentiableFunction Function
			{
				get
				{
					return this._function;
				}
			}

			// Token: 0x060016A2 RID: 5794 RVA: 0x00084B76 File Offset: 0x00082D76
			public override float Eval(ref VBuffer<float> input, ref VBuffer<float> gradient)
			{
				return this._function(ref input, ref gradient, this._progressProvider);
			}

			// Token: 0x04000DE9 RID: 3561
			private readonly DifferentiableFunction _function;
		}

		// Token: 0x02000443 RID: 1091
		public class PrematureConvergenceException : Optimizer.OptimizerException
		{
			// Token: 0x060016A3 RID: 5795 RVA: 0x00084B8B File Offset: 0x00082D8B
			public PrematureConvergenceException(Optimizer.OptimizerState state, string message)
				: base(state, message)
			{
			}
		}
	}
}
