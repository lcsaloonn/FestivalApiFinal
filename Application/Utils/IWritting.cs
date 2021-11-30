namespace Application.Utils
{
    public interface IWritting <out TO, in TI>
    {
        TO Execute(TI dto);
    }
}