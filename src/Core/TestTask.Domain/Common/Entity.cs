﻿namespace TestTask.Domain.Common;

public abstract class Entity<T> where T : IValueId<T>
{
	public T Id { get; } = T.Create();
}