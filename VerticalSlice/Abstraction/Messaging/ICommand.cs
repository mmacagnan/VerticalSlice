using MediatR;

namespace VerticalSlice.Abstraction.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}