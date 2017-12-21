ALTER TABLE `bill_customer` 
DROP INDEX `UNIQUE_DOC_NO_CUSTOMER_ID` ,
ADD UNIQUE INDEX `UNIQUE_DOC_NO_CUSTOMER_ID` (`bill_id` ASC, `customer_id` ASC);

------------------------------ 20171214 ------------------------------
ALTER TABLE `bill` 
ADD COLUMN `quantity` INT(11) NULL DEFAULT NULL AFTER `id`,
ADD COLUMN `in_total_price` DECIMAL(10,2) NULL DEFAULT NULL AFTER `quantity`,
ADD COLUMN `out_total_price` DECIMAL(10,2) NULL DEFAULT NULL AFTER `in_total_price`,
ADD COLUMN `profit` DECIMAL(10,2) NULL DEFAULT NULL AFTER `out_total_price`;

ALTER TABLE `bill` 
CHANGE COLUMN `quantity` `quantity` INT(11) NULL DEFAULT 0 ,
CHANGE COLUMN `in_total_price` `in_total_price` DECIMAL(10,2) NULL DEFAULT 0 ,
CHANGE COLUMN `out_total_price` `out_total_price` DECIMAL(10,2) NULL DEFAULT 0 ,
CHANGE COLUMN `profit` `profit` DECIMAL(10,2) NULL DEFAULT 0 ;

ALTER TABLE `bill_customer` 
CHANGE COLUMN `quantity` `quantity` INT(11) NULL DEFAULT 0 ,
CHANGE COLUMN `in_total_price` `in_total_price` DECIMAL(10,2) NULL DEFAULT 0 ,
CHANGE COLUMN `out_total_price` `out_total_price` DECIMAL(10,2) NULL DEFAULT 0 ,
CHANGE COLUMN `profit` `profit` DECIMAL(10,2) NULL DEFAULT 0 ;


ALTER TABLE `bill_goods` 
CHANGE COLUMN `quantity` `quantity` INT(11) NULL DEFAULT 1 ,
CHANGE COLUMN `in_unit_price` `in_unit_price` DECIMAL(10,2) NULL DEFAULT 0 COMMENT '成本价(是否RMB取决于is_rmb字段)' ,
CHANGE COLUMN `out_unit_price` `out_unit_price` DECIMAL(10,2) NULL DEFAULT 0 COMMENT '卖出价(人民币)' ,
CHANGE COLUMN `in_total_price` `in_total_price` DECIMAL(10,2) NULL DEFAULT 0 COMMENT '总成本(人民币)' ,
CHANGE COLUMN `out_total_price` `out_total_price` DECIMAL(10,2) NULL DEFAULT 0 COMMENT '总收入(人民币)' ,
CHANGE COLUMN `profit` `profit` DECIMAL(10,2) NULL DEFAULT 0 COMMENT '利润(人民币)' ;

-- 更新空值
UPDATE bill 
SET 
    quantity = 0,
    in_total_price = 0,
    out_total_price = 0,
    profit = 0
WHERE
    quantity IS NULL;

UPDATE bill_customer 
SET 
    quantity = 0,
    in_total_price = 0,
    out_total_price = 0,
    profit = 0
WHERE
    quantity IS NULL;

-- 更新商品利润
UPDATE bill_goods a
        INNER JOIN
    bill b ON a.bill_id = b.id 
SET 
    a.in_total_price = a.in_unit_price * a.quantity * (CASE a.is_rmb
        WHEN 0 THEN b.tax_rate
        ELSE 1
    END),
    a.out_total_price = a.out_unit_price * a.quantity,
    a.profit = a.out_unit_price * a.quantity - a.in_unit_price * a.quantity * (CASE a.is_rmb
        WHEN 0 THEN b.tax_rate
        ELSE 1
    END);

-- 更新客户利润
UPDATE bill_customer a
        INNER JOIN
    (SELECT 
        bill_customer_id,
            SUM(quantity) quantity,
            SUM(in_total_price) in_total_price,
            SUM(out_total_price) out_total_price,
            SUM(profit) profit
    FROM
        bill_goods
    GROUP BY bill_customer_id) b ON a.id = b.bill_customer_id 
SET 
    a.quantity = b.quantity,
    a.in_total_price = b.in_total_price,
    a.out_total_price = b.out_total_price,
    a.profit = b.profit;

-- 更新账单利润
UPDATE bill a
        INNER JOIN
    (SELECT 
        bill_id,
            SUM(quantity) quantity,
            SUM(in_total_price) in_total_price,
            SUM(out_total_price) out_total_price,
            SUM(profit) profit
    FROM
        bill_customer
    GROUP BY bill_id) b ON a.id = b.bill_id 
SET 
    a.quantity = b.quantity,
    a.in_total_price = b.in_total_price,
    a.out_total_price = b.out_total_price,
    a.profit = b.profit

-- 修复关联数据
UPDATE `daigou`.`bill_customer` SET `customer_nick_name`='冯莘岚' WHERE `id`='150972604957091856';
UPDATE `daigou`.`bill_customer` SET `customer_nick_name`='冯莘岚' WHERE `id`='150972604957338159';
UPDATE `daigou`.`bill_customer` SET `customer_nick_name`='冯莘岚' WHERE `id`='150972604958015033';
UPDATE `daigou`.`bill_customer` SET `customer_nick_name`='冯莘岚' WHERE `id`='150972604958658385';
UPDATE `daigou`.`bill_customer` SET `customer_id`='150978364997204038', `customer_nick_name`='朱师静' WHERE `id`='151108298662964048';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151235452214308355' WHERE `id`='151169115550400538';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151235452214308355' WHERE `id`='151229691350718633';
UPDATE `daigou`.`bill_customer` SET `customer_id`='150978365006432022' WHERE `id`='151296060883983497';
UPDATE `daigou`.`bill_customer` SET `customer_nick_name`='胡翠莉' WHERE `id`='151296060883983497';
DELETE FROM `daigou`.`cus_customer` WHERE `id`='151301819715944724';
UPDATE `daigou`.`bill_customer` SET `customer_id`='150978364991835373', `customer_nick_name`='张园园（蓝色冰点）' WHERE `id`='151229759250261214';
DELETE FROM `daigou`.`cus_customer` WHERE `id`='151129934316126178';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151128779097270673' WHERE `id`='151122872263504423';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151129405441331084' WHERE `id`='151123637409632722';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151129445873704050' WHERE `id`='151123685093831400';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151129806416605034' WHERE `id`='151124045829534716';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151129812537835752' WHERE `id`='151124052144100471';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151131068629544866' WHERE `id`='151125307556887443';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151353324840668586' WHERE `id`='151347559864360496';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151235387783464655' WHERE `id`='151229624942369200';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151301857273782515' WHERE `id`='151296098874495427';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151235383474826086' WHERE `id`='151229622898200570';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151235474797555437' WHERE `id`='151229711529722244';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151302109558141636' WHERE `id`='151296341664243267';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151353570755860673' WHERE `id`='151347810652512342';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151353531098573165' WHERE `id`='151347769579137584';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151353525867857380' WHERE `id`='151347765447528261';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151353482864777156' WHERE `id`='151347722319255481';
UPDATE `daigou`.`bill_customer` SET `customer_id`='151353478739241224' WHERE `id`='151347717911730340';
-- 批量更新
UPDATE bill_customer a
        INNER JOIN
    cus_customer b ON a.customer_nick_name = b.nick_name 
SET 
    a.customer_id = b.id
WHERE
    a.id IN ('151347713937219943' , '151347664474606944',
        '151347660668949900',
        '151322414381364100',
        '151322062452302040',
        '151322059573275882',
        '151322058916046110',
        '151322054972336313',
        '151322052945156252',
        '151322049031752040',
        '151322048208776224',
        '151322047116704873',
        '151322046348686553',
        '151322045656236786',
        '151322044865806833',
        '151322039238761632',
        '151322021362713657',
        '151298152255490764',
        '151298144961622146',
        '151298139317486542',
        '151296460958361429',
        '151296338475520226',
        '151296166637668593',
        '151296153836840968',
        '151296080107882677',
        '151296070361676076',
        '151260873871343761',
        '151254815921624062',
        '151254598016483587',
        '151246292458797120',
        '151230275984985865',
        '151229801267923594',
        '151229718637779033',
        '151229707467407159',
        '151229704168919881',
        '151229701182963257',
        '151229567419427779',
        '151229551369859494',
        '151169873919296593',
        '151169560772563677',
        '151169442247080551',
        '151169405850556645',
        '151169034219040861',
        '151168933934332632')
        AND b.enterprise_id = '18640650';