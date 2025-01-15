using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x02000005 RID: 5
	internal abstract class ArithmeticEvaluator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020C4 File Offset: 0x000002C4
		protected object CastToStrongType(object value)
		{
			checked
			{
				object obj;
				if (Information.IsNothing(RuntimeHelpers.GetObjectValue(value)))
				{
					obj = null;
				}
				else
				{
					Type type = value.GetType();
					if (!type.IsEnum)
					{
						string name = type.Name;
						uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
						if (num <= 2313474264U)
						{
							if (num <= 760156106U)
							{
								if (num <= 441298783U)
								{
									if (num <= 238028739U)
									{
										if (num != 103661943U)
										{
											if (num != 148287640U)
											{
												if (num != 238028739U)
												{
													goto IL_0AC4;
												}
												if (Operators.CompareString(name, "Boolean[]", false) != 0)
												{
													goto IL_0AC4;
												}
												return (bool[])value;
											}
											else
											{
												if (Operators.CompareString(name, "Guid[]", false) != 0)
												{
													goto IL_0AC4;
												}
												return (Guid[])value;
											}
										}
										else
										{
											if (Operators.CompareString(name, "SqlGeometry", false) != 0)
											{
												goto IL_0AC4;
											}
											goto IL_0A7C;
										}
									}
									else if (num != 423635464U)
									{
										if (num != 425125395U)
										{
											if (num != 441298783U)
											{
												goto IL_0AC4;
											}
											if (Operators.CompareString(name, "UInt64[]", false) != 0)
											{
												goto IL_0AC4;
											}
											return (ulong[])value;
										}
										else
										{
											if (Operators.CompareString(name, "DateTimeOffset", false) != 0)
											{
												goto IL_0AC4;
											}
											return (DateTimeOffset)value;
										}
									}
									else
									{
										if (Operators.CompareString(name, "SByte", false) != 0)
										{
											goto IL_0AC4;
										}
										return Convert.ToSByte(RuntimeHelpers.GetObjectValue(value));
									}
								}
								else if (num <= 627704921U)
								{
									if (num != 453818585U)
									{
										if (num != 468055569U)
										{
											if (num != 627704921U)
											{
												goto IL_0AC4;
											}
											if (Operators.CompareString(name, "FieldImpl", false) != 0)
											{
												goto IL_0AC4;
											}
										}
										else
										{
											if (Operators.CompareString(name, "Single[]", false) != 0)
											{
												goto IL_0AC4;
											}
											return (float[])value;
										}
									}
									else if (Operators.CompareString(name, "GlobalsImpl", false) != 0)
									{
										goto IL_0AC4;
									}
								}
								else if (num <= 679076413U)
								{
									if (num != 645321800U)
									{
										if (num != 679076413U)
										{
											goto IL_0AC4;
										}
										if (Operators.CompareString(name, "Char", false) != 0)
										{
											goto IL_0AC4;
										}
										return Convert.ToChar(RuntimeHelpers.GetObjectValue(value));
									}
									else
									{
										if (Operators.CompareString(name, "TimeSpan[]", false) != 0)
										{
											goto IL_0AC4;
										}
										return (TimeSpan[])value;
									}
								}
								else if (num != 697196164U)
								{
									if (num != 760156106U)
									{
										goto IL_0AC4;
									}
									if (Operators.CompareString(name, "Object[]", false) != 0)
									{
										goto IL_0AC4;
									}
									object[] array = (object[])value;
									int num2 = array.Length - 1;
									for (int i = 0; i <= num2; i++)
									{
										array[i] = RuntimeHelpers.GetObjectValue(this.CastToStrongType(RuntimeHelpers.GetObjectValue(array[i])));
									}
									return array;
								}
								else
								{
									if (Operators.CompareString(name, "Int64", false) != 0)
									{
										goto IL_0AC4;
									}
									return Convert.ToInt64(RuntimeHelpers.GetObjectValue(value));
								}
							}
							else if (num <= 1324880019U)
							{
								if (num <= 1189326818U)
								{
									if (num != 765439473U)
									{
										if (num != 963637354U)
										{
											if (num != 1189326818U)
											{
												goto IL_0AC4;
											}
											if (Operators.CompareString(name, "UInt16[]", false) != 0)
											{
												goto IL_0AC4;
											}
											return (ushort[])value;
										}
										else if (Operators.CompareString(name, "FieldsImpl", false) != 0)
										{
											goto IL_0AC4;
										}
									}
									else
									{
										if (Operators.CompareString(name, "Int16", false) != 0)
										{
											goto IL_0AC4;
										}
										return Convert.ToInt16(RuntimeHelpers.GetObjectValue(value));
									}
								}
								else if (num != 1190258704U)
								{
									if (num != 1323747186U)
									{
										if (num != 1324880019U)
										{
											goto IL_0AC4;
										}
										if (Operators.CompareString(name, "UInt64", false) != 0)
										{
											goto IL_0AC4;
										}
										return Convert.ToUInt64(RuntimeHelpers.GetObjectValue(value));
									}
									else
									{
										if (Operators.CompareString(name, "UInt16", false) != 0)
										{
											goto IL_0AC4;
										}
										return Convert.ToUInt16(RuntimeHelpers.GetObjectValue(value));
									}
								}
								else if (Operators.CompareString(name, "UserImpl", false) != 0)
								{
									goto IL_0AC4;
								}
							}
							else if (num <= 1637158326U)
							{
								if (num != 1431672590U)
								{
									if (num != 1615808600U)
									{
										if (num != 1637158326U)
										{
											goto IL_0AC4;
										}
										if (Operators.CompareString(name, "LookupsImpl", false) != 0)
										{
											goto IL_0AC4;
										}
									}
									else
									{
										if (Operators.CompareString(name, "String", false) != 0)
										{
											goto IL_0AC4;
										}
										return Convert.ToString(RuntimeHelpers.GetObjectValue(value));
									}
								}
								else if (Operators.CompareString(name, "ParameterImpl", false) != 0)
								{
									goto IL_0AC4;
								}
							}
							else if (num <= 1942868288U)
							{
								if (num != 1763994747U)
								{
									if (num != 1942868288U)
									{
										goto IL_0AC4;
									}
									if (Operators.CompareString(name, "VariablesImpl", false) != 0)
									{
										goto IL_0AC4;
									}
								}
								else if (Operators.CompareString(name, "ParametersImpl", false) != 0)
								{
									goto IL_0AC4;
								}
							}
							else if (num != 1977367276U)
							{
								if (num != 2313474264U)
								{
									goto IL_0AC4;
								}
								if (Operators.CompareString(name, "UInt32[]", false) != 0)
								{
									goto IL_0AC4;
								}
								return (uint[])value;
							}
							else
							{
								if (Operators.CompareString(name, "String[]", false) != 0)
								{
									goto IL_0AC4;
								}
								return (string[])value;
							}
						}
						else if (num <= 3426307091U)
						{
							if (num <= 2711245919U)
							{
								if (num <= 2417551388U)
								{
									if (num != 2341828857U)
									{
										if (num != 2386971688U)
										{
											if (num != 2417551388U)
											{
												goto IL_0AC4;
											}
											if (Operators.CompareString(name, "TimeSpan", false) != 0)
											{
												goto IL_0AC4;
											}
											return (TimeSpan)value;
										}
										else
										{
											if (Operators.CompareString(name, "Double", false) != 0)
											{
												goto IL_0AC4;
											}
											return Convert.ToDouble(RuntimeHelpers.GetObjectValue(value));
										}
									}
									else
									{
										if (Operators.CompareString(name, "Int16[]", false) != 0)
										{
											goto IL_0AC4;
										}
										return (short[])value;
									}
								}
								else if (num != 2615964816U)
								{
									if (num != 2642490659U)
									{
										if (num != 2711245919U)
										{
											goto IL_0AC4;
										}
										if (Operators.CompareString(name, "Int32", false) != 0)
										{
											goto IL_0AC4;
										}
										return Convert.ToInt32(RuntimeHelpers.GetObjectValue(value));
									}
									else
									{
										if (Operators.CompareString(name, "Byte[]", false) != 0)
										{
											goto IL_0AC4;
										}
										return (byte[])value;
									}
								}
								else
								{
									if (Operators.CompareString(name, "DateTime", false) != 0)
									{
										goto IL_0AC4;
									}
									return (DateTime)value;
								}
							}
							else if (num <= 2898774828U)
							{
								if (num != 2779444460U)
								{
									if (num != 2866489215U)
									{
										if (num != 2898774828U)
										{
											goto IL_0AC4;
										}
										if (Operators.CompareString(name, "Guid", false) != 0)
										{
											goto IL_0AC4;
										}
										return (Guid)value;
									}
									else
									{
										if (Operators.CompareString(name, "SqlHierarchyId", false) != 0)
										{
											goto IL_0AC4;
										}
										goto IL_0A7C;
									}
								}
								else
								{
									if (Operators.CompareString(name, "Decimal", false) != 0)
									{
										goto IL_0AC4;
									}
									return Convert.ToDecimal(RuntimeHelpers.GetObjectValue(value));
								}
							}
							else if (num <= 3257435823U)
							{
								if (num != 2989221208U)
								{
									if (num != 3257435823U)
									{
										goto IL_0AC4;
									}
									if (Operators.CompareString(name, "SqlGeography", false) != 0)
									{
										goto IL_0AC4;
									}
									goto IL_0A7C;
								}
								else
								{
									if (Operators.CompareString(name, "Decimal[]", false) != 0)
									{
										goto IL_0AC4;
									}
									return (decimal[])value;
								}
							}
							else if (num != 3409549631U)
							{
								if (num != 3426307091U)
								{
									goto IL_0AC4;
								}
								if (Operators.CompareString(name, "AggregatesImpl", false) != 0)
								{
									goto IL_0AC4;
								}
							}
							else
							{
								if (Operators.CompareString(name, "Byte", false) != 0)
								{
									goto IL_0AC4;
								}
								return Convert.ToByte(RuntimeHelpers.GetObjectValue(value));
							}
						}
						else if (num <= 3777191964U)
						{
							if (num <= 3585005533U)
							{
								if (num != 3509231420U)
								{
									if (num != 3538687084U)
									{
										if (num != 3585005533U)
										{
											goto IL_0AC4;
										}
										if (Operators.CompareString(name, "Char[]", false) != 0)
										{
											goto IL_0AC4;
										}
										return (char[])value;
									}
									else
									{
										if (Operators.CompareString(name, "UInt32", false) != 0)
										{
											goto IL_0AC4;
										}
										return Convert.ToUInt32(RuntimeHelpers.GetObjectValue(value));
									}
								}
								else
								{
									if (Operators.CompareString(name, "Double[]", false) != 0)
									{
										goto IL_0AC4;
									}
									return (double[])value;
								}
							}
							else if (num <= 3724734455U)
							{
								if (num != 3646816451U)
								{
									if (num != 3724734455U)
									{
										goto IL_0AC4;
									}
									if (Operators.CompareString(name, "VariableImpl", false) != 0)
									{
										goto IL_0AC4;
									}
								}
								else
								{
									if (Operators.CompareString(name, "Int32[]", false) != 0)
									{
										goto IL_0AC4;
									}
									return (int[])value;
								}
							}
							else if (num != 3774159766U)
							{
								if (num != 3777191964U)
								{
									goto IL_0AC4;
								}
								if (Operators.CompareString(name, "SByte[]", false) != 0)
								{
									goto IL_0AC4;
								}
								return (sbyte[])value;
							}
							else
							{
								if (Operators.CompareString(name, "DBNull", false) != 0)
								{
									goto IL_0AC4;
								}
								return (DBNull)value;
							}
						}
						else if (num <= 3965872478U)
						{
							if (num != 3851314394U)
							{
								if (num != 3929735716U)
								{
									if (num != 3965872478U)
									{
										goto IL_0AC4;
									}
									if (Operators.CompareString(name, "DBNull[]", false) != 0)
									{
										goto IL_0AC4;
									}
									return (DBNull[])value;
								}
								else
								{
									if (Operators.CompareString(name, "DateTime[]", false) != 0)
									{
										goto IL_0AC4;
									}
									return (DateTime[])value;
								}
							}
							else
							{
								if (Operators.CompareString(name, "Object", false) != 0)
								{
									goto IL_0AC4;
								}
								return value;
							}
						}
						else if (num <= 4051133705U)
						{
							if (num != 3969205087U)
							{
								if (num != 4051133705U)
								{
									goto IL_0AC4;
								}
								if (Operators.CompareString(name, "Single", false) != 0)
								{
									goto IL_0AC4;
								}
								return Convert.ToSingle(RuntimeHelpers.GetObjectValue(value));
							}
							else
							{
								if (Operators.CompareString(name, "Boolean", false) != 0)
								{
									goto IL_0AC4;
								}
								return Convert.ToBoolean(RuntimeHelpers.GetObjectValue(value));
							}
						}
						else if (num != 4159848671U)
						{
							if (num != 4284061936U)
							{
								goto IL_0AC4;
							}
							if (Operators.CompareString(name, "Int64[]", false) != 0)
							{
								goto IL_0AC4;
							}
							return (long[])value;
						}
						else
						{
							if (Operators.CompareString(name, "DateTimeOffset[]", false) != 0)
							{
								goto IL_0AC4;
							}
							return (DateTimeOffset[])value;
						}
						return value;
						IL_0A7C:
						return value;
						IL_0AC4:
						throw new NotImplementedException(string.Format("CastToStrongType for <{0}> is not implemented.", name));
					}
					obj = (Enum)value;
				}
				return obj;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002BA8 File Offset: 0x00000DA8
		protected void ValidateStrongType(List<object> arguments)
		{
			try
			{
				foreach (object obj in arguments)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(obj);
					this.CastToStrongType(RuntimeHelpers.GetObjectValue(objectValue));
				}
			}
			finally
			{
				List<object>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
		}
	}
}
