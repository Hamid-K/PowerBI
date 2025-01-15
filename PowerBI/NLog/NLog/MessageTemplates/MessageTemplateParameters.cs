using System;
using System.Collections;
using System.Collections.Generic;
using NLog.Common;
using NLog.Internal;

namespace NLog.MessageTemplates
{
	// Token: 0x02000086 RID: 134
	public sealed class MessageTemplateParameters : IEnumerable<MessageTemplateParameter>, IEnumerable
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00018C61 File Offset: 0x00016E61
		public IEnumerator<MessageTemplateParameter> GetEnumerator()
		{
			return this._parameters.GetEnumerator();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00018C6E File Offset: 0x00016E6E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._parameters.GetEnumerator();
		}

		// Token: 0x17000190 RID: 400
		public MessageTemplateParameter this[int index]
		{
			get
			{
				return this._parameters[index];
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00018C89 File Offset: 0x00016E89
		public int Count
		{
			get
			{
				return this._parameters.Count;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00018C96 File Offset: 0x00016E96
		public bool IsPositional { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00018C9E File Offset: 0x00016E9E
		internal bool IsValidTemplate { get; }

		// Token: 0x06000975 RID: 2421 RVA: 0x00018CA8 File Offset: 0x00016EA8
		internal MessageTemplateParameters(string message, object[] parameters)
		{
			bool flag = parameters != null && parameters.Length != 0;
			bool flag2 = flag;
			bool flag3 = !flag;
			IList<MessageTemplateParameter> list2;
			if (!flag)
			{
				IList<MessageTemplateParameter> list = ArrayHelper.Empty<MessageTemplateParameter>();
				list2 = list;
			}
			else
			{
				list2 = MessageTemplateParameters.ParseMessageTemplate(message, parameters, out flag2, out flag3);
			}
			this._parameters = list2;
			this.IsPositional = flag2;
			this.IsValidTemplate = flag3;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00018CF9 File Offset: 0x00016EF9
		internal MessageTemplateParameters(IList<MessageTemplateParameter> templateParameters, string message, object[] parameters)
		{
			this._parameters = templateParameters ?? ArrayHelper.Empty<MessageTemplateParameter>();
			if (parameters != null && this._parameters.Count != parameters.Length)
			{
				this.IsValidTemplate = false;
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00018D2C File Offset: 0x00016F2C
		private static IList<MessageTemplateParameter> ParseMessageTemplate(string template, object[] parameters, out bool isPositional, out bool isValidTemplate)
		{
			isPositional = true;
			isValidTemplate = true;
			List<MessageTemplateParameter> list = new List<MessageTemplateParameter>(parameters.Length);
			IList<MessageTemplateParameter> list2;
			try
			{
				short num = 0;
				TemplateEnumerator templateEnumerator = new TemplateEnumerator(template);
				while (templateEnumerator.MoveNext())
				{
					if (templateEnumerator.Current.Literal.Skip != 0)
					{
						Hole hole = templateEnumerator.Current.Hole;
						if ((hole.Index != -1) & isPositional)
						{
							num += 1;
							object holeValueSafe = MessageTemplateParameters.GetHoleValueSafe(parameters, hole.Index);
							list.Add(new MessageTemplateParameter(hole.Name, holeValueSafe, hole.Format, hole.CaptureType));
						}
						else
						{
							if (isPositional)
							{
								isPositional = false;
								if (num != 0)
								{
									templateEnumerator = new TemplateEnumerator(template);
									num = 0;
									list.Clear();
									continue;
								}
							}
							object holeValueSafe2 = MessageTemplateParameters.GetHoleValueSafe(parameters, num);
							list.Add(new MessageTemplateParameter(hole.Name, holeValueSafe2, hole.Format, hole.CaptureType));
							num += 1;
						}
					}
				}
				if (list.Count != parameters.Length)
				{
					isValidTemplate = false;
				}
				list2 = list;
			}
			catch (Exception ex)
			{
				isValidTemplate = false;
				InternalLogger.Warn(ex, "Error when parsing a message.");
				list2 = list;
			}
			return list2;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00018E4C File Offset: 0x0001704C
		private static object GetHoleValueSafe(object[] parameters, short holeIndex)
		{
			if (parameters.Length <= (int)holeIndex)
			{
				return null;
			}
			return parameters[(int)holeIndex];
		}

		// Token: 0x04000237 RID: 567
		private readonly IList<MessageTemplateParameter> _parameters;
	}
}
