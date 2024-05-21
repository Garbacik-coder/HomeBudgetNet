UPDATE Spending
SET
    name = @name,
    value = @value,
    category = @category,
    date = @date
WHERE id = @id