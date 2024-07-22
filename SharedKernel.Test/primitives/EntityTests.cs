using SharedKernel.primitives;

namespace SharedKernel.Test.primitives;

public class EntityTests
{

    class TestEntity : Entity
    {
        public TestEntity(Guid id) : base(id)
        {
        }
    }
    [Fact]
    public void ShouldCreate_Entity_WithCorrectParameters()
    {
        // Given
        var id = Guid.NewGuid();
        
        // When
        var entity = new TestEntity(id);
        // Then
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void ShouldReturnTrue_WhenComparingTwoEntitiesWithSameId()
    {
        // Given
        var id = Guid.NewGuid();
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);
        
        // When
        var result = entity1.Equals(entity2);
        
        // Then
        Assert.True(result);
    }

    [Fact]
    public void ShouldReturnFalse_WhenComparingTwoEntitiesWithDifferentId()
    {
        // Given
        var entity1 = new TestEntity(Guid.NewGuid());
        var entity2 = new TestEntity(Guid.NewGuid());
        
        // When
        var result = entity1.Equals(entity2);
        
        // Then
        Assert.False(result);
    }
}
