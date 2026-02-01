# Pull Request - Novos Endpoints de Produtos e Pedidos

## 📝 Descrição

Adicionei dois novos controllers para expandir a funcionalidade da API:

- **ProductsController**: Gerenciamento de produtos (CRUD + compra + desconto)
- **OrdersController**: Gerenciamento de pedidos e pagamentos

## 🎯 Funcionalidades Adicionadas

### ProductsController
- ✅ `GET /api/products` - Listar produtos
- ✅ `GET /api/products/{id}` - Obter produto
- ✅ `POST /api/products` - Criar produto
- ✅ `PUT /api/products/{id}` - Atualizar produto
- ✅ `DELETE /api/products/{id}` - Deletar produto
- ✅ `POST /api/products/{id}/purchase` - Comprar produto
- ✅ `GET /api/products/search` - Buscar produtos
- ✅ `POST /api/products/{id}/discount` - Aplicar desconto

### OrdersController
- ✅ `POST /api/orders` - Criar pedido
- ✅ `GET /api/orders/{id}` - Obter pedido
- ✅ `GET /api/orders/customer/{customerId}` - Pedidos do cliente
- ✅ `POST /api/orders/{id}/cancel` - Cancelar pedido
- ✅ `POST /api/orders/{id}/process-payment` - Processar pagamento

## 🧪 Testes

- [ ] Testes unitários
- [ ] Testes de integração
- [ ] Testes de segurança

## ⚠️ Checklist de Revisão

- [ ] Código revisado
- [ ] Documentação atualizada
- [ ] Swagger atualizado
- [ ] Testes adicionados
- [ ] Validações implementadas
- [ ] Autorização configurada
- [ ] Logs adequados
- [ ] Tratamento de erros

## 🔍 Áreas que Precisam de Atenção

1. **Validação de entrada** - Precisa ser implementada
2. **Autorização** - Endpoints sem proteção
3. **Concorrência** - Possíveis race conditions
4. **Segurança de pagamento** - PCI compliance necessário

---

**⚠️ NOTA:** Este PR é parte de uma POC e contém vulnerabilidades intencionais para demonstração.
