

public interface IInteractable
{
    void Interact(IUnit unit, InteractType interactType);
    
}

public enum InteractType
{
    GIVE, TAKE
}
