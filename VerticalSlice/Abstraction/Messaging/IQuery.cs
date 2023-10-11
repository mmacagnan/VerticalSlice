using MediatR;

namespace VerticalSlice.Abstraction.Messaging;

public interface IQuery<out TResponse>
    : IRequest<TResponse>
{
}