using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using NLog.Common;
using NLog.Internal;

namespace NLog.Conditions
{
	// Token: 0x020001AB RID: 427
	internal sealed class ConditionMethodExpression : ConditionExpression
	{
		// Token: 0x06001310 RID: 4880 RVA: 0x00033A1C File Offset: 0x00031C1C
		public ConditionMethodExpression(string conditionMethodName, MethodInfo methodInfo, IEnumerable<ConditionExpression> methodParameters)
		{
			this.MethodInfo = methodInfo;
			this._conditionMethodName = conditionMethodName;
			this.MethodParameters = new List<ConditionExpression>(methodParameters).AsReadOnly();
			ParameterInfo[] parameters = this.MethodInfo.GetParameters();
			if (parameters.Length != 0 && parameters[0].ParameterType == typeof(LogEventInfo))
			{
				this._acceptsLogEvent = true;
			}
			int num = this.MethodParameters.Count;
			if (this._acceptsLogEvent)
			{
				num++;
			}
			int num2;
			int num3;
			ConditionMethodExpression.CountParmameters(parameters, out num2, out num3);
			if (num < num2 || num > parameters.Length)
			{
				string text;
				if (num3 > 0)
				{
					text = string.Format(CultureInfo.InvariantCulture, "Condition method '{0}' requires between {1} and {2} parameters, but passed {3}.", new object[] { conditionMethodName, num2, parameters.Length, num });
				}
				else
				{
					text = string.Format(CultureInfo.InvariantCulture, "Condition method '{0}' requires {1} parameters, but passed {2}.", new object[] { conditionMethodName, num2, num });
				}
				InternalLogger.Error(text);
				throw new ConditionParseException(text);
			}
			this.CreateBoundMethod(parameters);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00033B2C File Offset: 0x00031D2C
		private void CreateBoundMethod(ParameterInfo[] formalParameters)
		{
			this._lateBoundMethod = ReflectionHelpers.CreateLateBoundMethod(this.MethodInfo);
			if (formalParameters.Length > this.MethodParameters.Count)
			{
				this._lateBoundMethodDefaultParameters = new object[formalParameters.Length - this.MethodParameters.Count];
				for (int i = this.MethodParameters.Count; i < formalParameters.Length; i++)
				{
					ParameterInfo parameterInfo = formalParameters[i];
					this._lateBoundMethodDefaultParameters[i - this.MethodParameters.Count] = parameterInfo.DefaultValue;
				}
				return;
			}
			this._lateBoundMethodDefaultParameters = null;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00033BB4 File Offset: 0x00031DB4
		private static void CountParmameters(ParameterInfo[] formalParameters, out int requiredParametersCount, out int optionalParametersCount)
		{
			requiredParametersCount = 0;
			optionalParametersCount = 0;
			for (int i = 0; i < formalParameters.Length; i++)
			{
				if (formalParameters[i].IsOptional)
				{
					optionalParametersCount++;
				}
				else
				{
					requiredParametersCount++;
				}
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x00033BEF File Offset: 0x00031DEF
		// (set) Token: 0x06001314 RID: 4884 RVA: 0x00033BF7 File Offset: 0x00031DF7
		public MethodInfo MethodInfo { get; private set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00033C00 File Offset: 0x00031E00
		// (set) Token: 0x06001316 RID: 4886 RVA: 0x00033C08 File Offset: 0x00031E08
		public IList<ConditionExpression> MethodParameters { get; private set; }

		// Token: 0x06001317 RID: 4887 RVA: 0x00033C14 File Offset: 0x00031E14
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._conditionMethodName);
			stringBuilder.Append("(");
			string text = string.Empty;
			for (int i = 0; i < this.MethodParameters.Count; i++)
			{
				ConditionExpression conditionExpression = this.MethodParameters[i];
				stringBuilder.Append(text);
				stringBuilder.Append(conditionExpression);
				text = ", ";
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00033C94 File Offset: 0x00031E94
		protected override object EvaluateNode(LogEventInfo context)
		{
			int num = (this._acceptsLogEvent ? 1 : 0);
			object[] lateBoundMethodDefaultParameters = this._lateBoundMethodDefaultParameters;
			int num2 = ((lateBoundMethodDefaultParameters != null) ? lateBoundMethodDefaultParameters.Length : 0);
			object[] array = new object[this.MethodParameters.Count + num + num2];
			for (int i = 0; i < this.MethodParameters.Count; i++)
			{
				ConditionExpression conditionExpression = this.MethodParameters[i];
				array[i + num] = conditionExpression.Evaluate(context);
			}
			if (this._acceptsLogEvent)
			{
				array[0] = context;
			}
			if (this._lateBoundMethodDefaultParameters != null)
			{
				for (int j = this._lateBoundMethodDefaultParameters.Length - 1; j >= 0; j--)
				{
					array[array.Length - j - 1] = this._lateBoundMethodDefaultParameters[j];
				}
			}
			return this._lateBoundMethod(null, array);
		}

		// Token: 0x04000514 RID: 1300
		private readonly bool _acceptsLogEvent;

		// Token: 0x04000515 RID: 1301
		private readonly string _conditionMethodName;

		// Token: 0x04000517 RID: 1303
		private ReflectionHelpers.LateBoundMethod _lateBoundMethod;

		// Token: 0x04000518 RID: 1304
		private object[] _lateBoundMethodDefaultParameters;
	}
}
